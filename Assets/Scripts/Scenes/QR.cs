using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class QR : MonoBehaviour
{
	public GameObject Manager;
	public GameObject QRScene;
	public GameObject Create;
	public GameObject CreateTop;
	public GameObject Scan;
	public GameObject ScanTop;
	public GameObject Export;
	public GameObject ExportTop;
	public RawImage QROutput;
	public RawImage Screen;
	WebCamTexture Camera;
	public Text CameraText;
	public Text StatusText;
	public Text DeviceText;
	public Text FilesText;
	public GameObject Display;
	public GameObject RescanButton;
	Color[] Colors;
	Color[] ColorR;
	Color[] ColorG;
	Color[] ColorB;
	public Texture2D Scr;
	Texture2D Img1;
	Texture2D Img2;
	Texture2D Img3;
	Result result1;
	Result result2;
	Result result3;
	int Percent;
	bool QR1;
	bool QR2;
	bool QR3;
	public Toggle UseHighDensity;
	public Toggle UseCompression;
	public Toggle UseHighDensityScan;
	bool hasCamera;
	readonly int MaxSize = 300;
	readonly int MaxSize3 = 900;

	string CurrentUUID;
	string DataString;
	int ThisQR;
	int TotalQR;
	bool hasScanned;
	int DevicesScanned;
	List<string> Devices = new List<string>();
	int TotalFiles;

	bool Generated;
	bool Split;
	int CurrentQR;
	int MaxQR;
	Texture2D[] QRarr;
	public Text Progress;
	public GameObject Multi;

	public void Wake()
	{
		UseHighDensity.isOn = true;
		UseCompression.isOn = true;
		UseHighDensityScan.isOn = true;
		CurrentQR = 0;
		Generated = false;
		Progress.text = "";
		CurrentUUID = "None";
		DataString = "";
		Multi.SetActive(false);
		Create.SetActive(false);
		CreateTop.SetActive(false);
		Scan.SetActive(false);
		ScanTop.SetActive(false);
		hasCamera = false;
		RescanButton.SetActive(false);
		CameraText.text = "Camera Type: NO CAMERA DETECTED";
		StartCoroutine(StartCam());
		StatusText.text = "Status: Ready to scan";
		FilesText.text = "Files: 0";
		DeviceText.text = "Devices: 0";
	}

	public void GenerateQR()
	{
		CurrentQR = 0;
		string Data = "";
		List<string> AllFiles = GetFiles();
		List<string> InfoFiles = new List<string>();
		List<FilesClass> CompFiles = new List<FilesClass>();
		int ErrorsCount = 0;
		int Current = 1;
		foreach (string Directory in AllFiles)
		{
			string check = FileTest.FileCheck(Directory);
			switch (check)
			{
				case "Future":
					ErrorsCount++;
					break;
				case "RS2":
					byte[] bytesfile = File.ReadAllBytes(Directory);
					string JSONfile = Encoding.ASCII.GetString(bytesfile);
					FilesClass Files;
					Files = JsonUtility.FromJson<FilesClass>(JSONfile);
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(Files);
					break;
				case "RS1.5":
					ErrorsCount++;
					break;
				case "RS1":
					string[] ScoutFiles = RS1Files.LoadRS1Files(Directory);
					FilesClass FilesRS1 = new FilesClass
					{
						CreatedWith = "RS1",
						LastOpenedWith = "Unknown",
						Scouter = "Unknown",
						QuestionsInt = 11,
						QuestionsType = new int[11] { 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 },
						Questions = new string[11] { "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Comment" },
						Answers = new string[11] { "", ScoutFiles[0], ScoutFiles[1], "", ScoutFiles[2], ScoutFiles[3], ScoutFiles[4], "", ScoutFiles[5], ScoutFiles[6], ScoutFiles[7] }
					};
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(FilesRS1);
					break;
				case "Error":
					ErrorsCount++;
					break;
			}
			Current++;
		}
		FilesQRClass Package = new FilesQRClass
		{
			FilesCount = InfoFiles.Count,
			FilesInfo = InfoFiles.Select(i => i).ToArray(),
			Files = CompFiles.Select(i => i).ToArray()
		};
		Data = JsonUtility.ToJson(Package, false);
		if (UseHighDensity.isOn)
		{
			if (UseCompression.isOn)
			{
				Compress(Data, true, true);
			}
			else
			{
				Compress(Data, true, false);
			}
		}
		else
		{
			if (UseCompression.isOn)
			{
				Compress(Data, false, true);
			}
			else
			{
				Compress(Data, false, false);
			}
		}
	}

	Texture2D GenerateBarcode(string data, int width, int height, Color color)
	{
		BitMatrix bitMatrix = new MultiFormatWriter().encode(data, BarcodeFormat.QR_CODE, width, height);
		Color[] pixels = new Color[bitMatrix.Width * bitMatrix.Height];
		int pos = 0;
		for (int y = 0; y < bitMatrix.Height; y++)
		{
			for (int x = 0; x < bitMatrix.Width; x++)
			{
				pixels[pos++] = bitMatrix[x, y] ? color : Color.white;
			}
		}
		Texture2D Texture = new Texture2D(bitMatrix.Width, bitMatrix.Height);
		Texture.SetPixels(pixels);
		Texture.Apply();
		return Texture;
	}

	public void ResetScanner()
	{
		if (hasCamera == true)
		{
			StartCoroutine(StartCam());
			StatusText.text = "Status: Ready to scan";
			FilesText.text = "Files: 0";
			DeviceText.text = "Devices: 0";
			TotalFiles = 0;
			DevicesScanned = 0;
			CurrentUUID = "None";
			DataString = "";
			Devices = new List<string>();
			hasScanned = false;
			Camera.Play();
			RescanButton.SetActive(false);
		}
	}

	public void Rescan()
	{
		if (hasCamera == true)
		{
			StartCoroutine(StartCam());
			StatusText.text = "Status: Ready to scan";
			hasScanned = false;
			Camera.Play();
			RescanButton.SetActive(false);
		}
	}

	IEnumerator StartCam()
	{
		while (!Scan.activeSelf)
		{
			yield return null;
		}
		Color[] Colors = new Color[1] {Color.clear};
		Scr = new Texture2D(1, 1);
		Scr.SetPixels(Colors);
		Scr.Apply();
		Screen.texture = Scr;
		WebCamDevice[] cam_devices = WebCamTexture.devices;
		if (cam_devices.Length != 0)
		{
			hasCamera = true;
			Camera = new WebCamTexture(cam_devices[0].name);
			CameraText.text = "Initializing camera, Please wait...";
		}
		if (hasCamera == true)
		{
			Camera.requestedWidth = 7680;
			Camera.requestedHeight = 4320;
			Camera.Play();
			yield return new WaitForSeconds(2);
			CameraText.text = "Camera Type: " + cam_devices[0].name.ToString();
			DeCompress();
		}
	}

	void Compress(string String, bool HighDensity, bool Compression)
	{
		Generated = false;
		Progress.text = "";
		string CData;
		Split = false;
		Texture2D QRCode;
		if (HighDensity)
		{
			if (Compression)
			{
				CData = StringCompressor.CompressString(String);
			}
			else
			{
				CData = String;
			}
			if (CData.Length > MaxSize3)
			{
				Split = true;
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKey(KeyCode.S))
				{
					Split = true;
				}
				else
				{
					Split = false;
				}
			}
			if (!Split)
			{
				QRClass Data = new QRClass
				{
					SaveVersion = "v1",
					Master = false,
					UUID = SystemInfo.deviceUniqueIdentifier,
					ThisQR = 1,
					Compression = Compression,
					Data = CData
				};
				CData = JsonUtility.ToJson(Data, false);
				string[] Data3 = new string[3];
				int DataSize = CData.Length;
				int ChunkSize;
				while (DataSize % 3 != 0)
				{
					CData += " ";
					DataSize = CData.Length;
				}
				ChunkSize = DataSize / 3;
				int Chunk = 0;
				for (int i = 0; i < DataSize; i++)
				{
					if (i % ChunkSize == 0)
						Chunk++;
					Data3[Chunk - 1] += CData[i];
				}
				Color[] Img1 = GenerateBarcode(Data3[0], 1024, 1024, Color.cyan).GetPixels();
				Color[] Img2 = GenerateBarcode(Data3[1], 1024, 1024, Color.magenta).GetPixels();
				Color[] Img3 = GenerateBarcode(Data3[2], 1024, 1024, Color.yellow).GetPixels();
				for (var i = 0; i < Img1.Length; ++i)
				{
					Img2[i] *= Img3[i];
					Img1[i] *= Img2[i];
				}
				QRCode = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
				QRCode.filterMode = FilterMode.Point;
				QRCode.SetPixels(Img1);
				QRCode.Apply();
				QRarr = new Texture2D[1];
				MaxQR = 0;
				QRarr[0] = QRCode;
			}
			else
			{
				int Part = 0;
				decimal D = CData.Length;
				int Blocks = (int)Math.Ceiling(D / MaxSize3);
				string[] SplitData = new string[Blocks];
				for (int i = 0; i < CData.Length; i++)
				{
					if (i % MaxSize3 == 0)
					{
						Part++;
					}
					SplitData[Part - 1] += CData[i];
				}
				QRarr = new Texture2D[Blocks + 1];
				QRClass Package = new QRClass
				{
					SaveVersion = "v1",
					Master = true,
					UUID = SystemInfo.deviceUniqueIdentifier,
					MaxFiles = Blocks
				};
				string Header = JsonUtility.ToJson(Package, false);
				string[] Header3 = new string[3];
				int HeaderSize = Header.Length;
				int ChunkHSize;
				while (HeaderSize % 3 != 0)
				{
					Header += " ";
					HeaderSize = Header.Length;
				}
				ChunkHSize = HeaderSize / 3;
				int HChunk = 0;
				for (int j = 0; j < HeaderSize; j++)
				{
					if (j % ChunkHSize == 0)
						HChunk++;
					Header3[HChunk - 1] += Header[j];
				}
				Color[] ImgH1 = GenerateBarcode(Header3[0], 1024, 1024, Color.cyan).GetPixels();
				Color[] ImgH2 = GenerateBarcode(Header3[1], 1024, 1024, Color.magenta).GetPixels();
				Color[] ImgH3 = GenerateBarcode(Header3[2], 1024, 1024, Color.yellow).GetPixels();
				for (var j = 0; j < ImgH1.Length; ++j)
				{
					ImgH2[j] *= ImgH3[j];
					ImgH1[j] *= ImgH2[j];
				}
				Texture2D QRHeader = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
				QRHeader.filterMode = FilterMode.Point;
				QRHeader.SetPixels(ImgH1);
				QRHeader.Apply();
				QRarr[0] = QRHeader;
				for (int i = 1; i < Blocks + 1; i++)
				{
					QRClass Data = new QRClass
					{
						SaveVersion = "v1",
						Master = false,
						UUID = SystemInfo.deviceUniqueIdentifier,
						ThisQR = i + 1,
						Compression = Compression,
						Data = SplitData[i - 1]
					};
					CData = JsonUtility.ToJson(Data, false);
					string[] Data3 = new string[3];
					int DataSize = CData.Length;
					int ChunkSize;
					while (DataSize % 3 != 0)
					{
						CData += " ";
						DataSize = CData.Length;
					}
					ChunkSize = DataSize / 3;
					int Chunk = 0;
					for (int j = 0; j < DataSize; j++)
					{
						if (j % ChunkSize == 0)
							Chunk++;
						Data3[Chunk - 1] += CData[j];
					}
					Color[] Img1 = GenerateBarcode(Data3[0], 1024, 1024, Color.cyan).GetPixels();
					Color[] Img2 = GenerateBarcode(Data3[1], 1024, 1024, Color.magenta).GetPixels();
					Color[] Img3 = GenerateBarcode(Data3[2], 1024, 1024, Color.yellow).GetPixels();
					for (var j = 0; j < Img1.Length; ++j)
					{
						Img2[j] *= Img3[j];
						Img1[j] *= Img2[j];
					}
					QRCode = new Texture2D(1024, 1024, TextureFormat.RGBA32, false)
					{
						filterMode = FilterMode.Point
					};
					QRCode.SetPixels(Img1);
					QRCode.Apply();
					QRarr[i] = QRCode;
				}
				MaxQR = Blocks;
			}
		}
		else
		{
			if (Compression)
			{
				CData = StringCompressor.CompressString(String);
			}
			else
			{
				CData = String;
			}
			if (CData.Length > MaxSize)
			{
				Split = true;
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKey(KeyCode.S))
				{
					Split = true;
				}
				else
				{
					Split = false;
				}
			}
			if (!Split)
			{
				QRClass Data = new QRClass
				{
					SaveVersion = "v1",
					Master = false,
					UUID = SystemInfo.deviceUniqueIdentifier,
					ThisQR = 1,
					Compression = Compression,
					Data = CData
				};
				CData = JsonUtility.ToJson(Data, false);
				Color[] Img = GenerateBarcode(CData, 1024, 1024, Color.black).GetPixels();
				QRCode = new Texture2D(1024, 1024, TextureFormat.RGBA32, false)
				{
					filterMode = FilterMode.Point
				};
				QRCode.SetPixels(Img);
				QRCode.Apply();
				QRarr = new Texture2D[1];
				MaxQR = 0;
				QRarr[0] = QRCode;
			}
			else
			{
				int Part = 0;
				decimal D = CData.Length;
				int Blocks = (int)Math.Ceiling(D / MaxSize);
				string[] SplitData = new string[Blocks];
				for (int i = 0; i < CData.Length; i++)
				{
					if (i % MaxSize == 0)
					{
						Part++;
					}
					SplitData[Part - 1] += CData[i];
				}
				QRarr = new Texture2D[Blocks + 1];
				QRClass Package = new QRClass
				{
					SaveVersion = "v1",
					Master = true,
					UUID = SystemInfo.deviceUniqueIdentifier,
					MaxFiles = Blocks
				};
				string Header = JsonUtility.ToJson(Package, false);
				Color[] Img1 = GenerateBarcode(Header, 1024, 1024, Color.black).GetPixels();
				Texture2D QRHeader = new Texture2D(1024, 1024, TextureFormat.RGBA32, false)
				{
					filterMode = FilterMode.Point
				};
				QRHeader.SetPixels(Img1);
				QRHeader.Apply();
				QRarr[0] = QRHeader;
				for (int i = 1; i < Blocks + 1; i++)
				{
					QRClass Data = new QRClass
					{
						SaveVersion = "v1",
						Master = false,
						UUID = SystemInfo.deviceUniqueIdentifier,
						ThisQR = i + 1,
						Compression = Compression,
						Data = SplitData[i - 1]
					};
					CData = JsonUtility.ToJson(Data, false);
					Color[] Img = GenerateBarcode(CData, 1024, 1024, Color.black).GetPixels();
					QRCode = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
					QRCode.filterMode = FilterMode.Point;
					QRCode.SetPixels(Img);
					QRCode.Apply();
					QRarr[i] = QRCode;
				}
				MaxQR = Blocks;
			}
		}
		Generated = true;
	}

	public void Last()
	{
		if (CurrentQR != 0)
		{
			CurrentQR--;
		}
	}

	public void Next()
	{
		if (CurrentQR != MaxQR)
		{
			CurrentQR++;
		}
	}

	void DeCompress()
	{
		hasScanned = false;
		RescanButton.SetActive(false);
		Display.SetActive(false);
		if (Camera != null)
		{
			Camera.Play();
			Display.SetActive(true);
			Scr = new Texture2D(Camera.width, Camera.height, TextureFormat.RGBA32, false);
			Img1 = new Texture2D(Camera.width, Camera.height, TextureFormat.RGBA32, false);
			Img2 = new Texture2D(Camera.width, Camera.height, TextureFormat.RGBA32, false);
			Img3 = new Texture2D(Camera.width, Camera.height, TextureFormat.RGBA32, false);
			Scr.filterMode = FilterMode.Point;
			Img1.filterMode = FilterMode.Point;
			Img2.filterMode = FilterMode.Point;
			Img3.filterMode = FilterMode.Point;
			Debug.Log(SystemInfo.processorCount);
			if (SystemInfo.processorCount < 4)
			{
				StartCoroutine(QRScanFallback(10));
			}
			else
			{
				StartCoroutine(QRScanManager(10));
			}
		}
	}

	IEnumerator QRScanFallback(float Framerate)
	{
		if (hasScanned == false && Scan.activeSelf)
		{
			Colors = Camera.GetPixels();
			ColorR = new Color[Camera.GetPixels().Length];
			ColorG = new Color[Camera.GetPixels().Length];
			ColorB = new Color[Camera.GetPixels().Length];
			Parallel.For(0, ColorR.Length, delegate (int i) { ColorR[i] = new Color(Colors[i].r, Colors[i].r, Colors[i].r); });
			Parallel.For(0, ColorG.Length, delegate (int i) { ColorG[i] = new Color(Colors[i].g, Colors[i].g, Colors[i].g); });
			Parallel.For(0, ColorB.Length, delegate (int i) { ColorB[i] = new Color(Colors[i].b, Colors[i].b, Colors[i].b); });
			Scr.SetPixels(Colors);
			Img1.SetPixels(ColorR);
			Img2.SetPixels(ColorG);
			Img3.SetPixels(ColorB);
			Scr.Apply();
			Img1.Apply();
			Img2.Apply();
			Img3.Apply();
			IBarcodeReader barcodeReader1 = new BarcodeReader();
			IBarcodeReader barcodeReader2 = new BarcodeReader();
			IBarcodeReader barcodeReader3 = new BarcodeReader();
			result1 = barcodeReader1.Decode(Img1.GetPixels32(), Img1.width, Img1.height);
			result2 = barcodeReader2.Decode(Img2.GetPixels32(), Img2.width, Img2.height);
			result3 = barcodeReader3.Decode(Img3.GetPixels32(), Img3.width, Img3.height);
			Percent = 0;
			if (result1 != null) { QR1 = true; Percent++; } else { QR1 = false; }
			if (result2 != null) { QR2 = true; Percent++; } else { QR2 = false; }
			if (result3 != null) { QR3 = true; Percent++; } else { QR3 = false; }
			Screen.texture = Scr;
			float Delay = 1 / Framerate;
			yield return new WaitForSeconds(Delay);
			StartCoroutine(QRScanFallback(Framerate));
		}
	}

	IEnumerator QRScanManager(float Framerate)
    {
		if (hasScanned == false && Scan.activeSelf)
		{
			Scr.SetPixels(Colors);
			Scr.Apply();
			Screen.texture = Scr;
			Colors = Camera.GetPixels();
			int CameraLen = Camera.GetPixels32().Length;
			for (int i = 0; i < 4; i++)
			{
				new Thread(delegate () { QRScan(i, Colors, CameraLen); }).Start();
			}
			float Delay = 1 / Framerate;
			yield return new WaitForSeconds(Delay);
			StartCoroutine(QRScanManager(10));
		}
	}

	void QRScan(int ThreadNum, Color[] Colors, int CameraLen)
	{
		Percent = 0;
		switch (ThreadNum)
		{
			case 1:
				ColorR = new Color[CameraLen];
				Parallel.For(0, ColorR.Length, delegate (int i) { ColorR[i] = new Color(Colors[i].r, Colors[i].r, Colors[i].r); });
				Texture2D PImg1 = Img1;
				PImg1.SetPixels(ColorR);
				PImg1.Apply();
				IBarcodeReader barcodeReader1 = new BarcodeReader();
				result1 = barcodeReader1.Decode(PImg1.GetPixels32(), PImg1.width, PImg1.height);
				if (result1 != null) { QR1 = true; Percent++; } else { QR1 = false; }
				break;
			case 2:
				ColorG = new Color[CameraLen];
				Parallel.For(0, ColorG.Length, delegate (int i) { ColorG[i] = new Color(Colors[i].g, Colors[i].g, Colors[i].g); });
				Texture2D PImg2 = Img2;
				PImg2.SetPixels(ColorG);
				PImg2.Apply();
				IBarcodeReader barcodeReader2 = new BarcodeReader();
				result2 = barcodeReader2.Decode(PImg2.GetPixels32(), PImg2.width, PImg2.height);
				if (result2 != null) { QR2 = true; Percent++; } else { QR2 = false; }
				break;
			case 3:
				ColorB = new Color[CameraLen];
				Parallel.For(0, ColorB.Length, delegate (int i) { ColorB[i] = new Color(Colors[i].b, Colors[i].b, Colors[i].b); });
				Texture2D PImg3 = Img3;
				PImg3.SetPixels(ColorB);
				PImg3.Apply();
				IBarcodeReader barcodeReader3 = new BarcodeReader();
				result3 = barcodeReader3.Decode(PImg3.GetPixels32(), PImg3.width, PImg3.height);
				if (result3 != null) { QR3 = true; Percent++; } else { QR3 = false; }
				break;
		}

	}

	public void Frame()
	{
		if (!QRScene.activeSelf)
		{
			SetWindow("Close");
		}
		if (Create.activeSelf)
		{
			if (Generated)
			{
				QROutput.texture = QRarr[CurrentQR];
				Progress.text = "Showing QR " + (CurrentQR + 1) + "/" + (MaxQR + 1);
				if (MaxQR > 0)
				{
					Multi.SetActive(true);
				}
				else
				{
					Multi.SetActive(false);
				}
			}
		}
		if (Scan.activeSelf)
		{
			if (hasCamera == true)
			{
				if (hasScanned == false)
				{
					string Part1;
					string Part2;
					if (CurrentUUID == "None")
					{
						if (Percent > 0)
						{
							Part1 = "Scanning,";
						}
						else
						{
							Part1 = "Ready to scan";
						}
					}
					else
					{
						if (Percent > 0)
						{
							Part1 = "Scanning,";
						}
						else
						{
							Part1 = "Ready to scan code " + (ThisQR) + "/" + (TotalQR + 1);
						}
					}
					if (Percent == 0)
					{
						Part2 = "";
					}
					else if (Percent == 1)
					{
						Part2 = "33%";
					}
					else if (Percent == 2)
					{
						Part2 = "66%";
					}
					else
					{
						Part2 = "100%... but this will never be seen :(";
					}
					StatusText.text = "Status: " + Part1 + " " + Part2;
					if (QR1 || QR2 || QR3)
					{
						string Result;
						if (UseHighDensityScan.isOn)
						{
							if (QR1 && QR2 && QR3)
							{
								Result = result1.Text + result2.Text + result3.Text;
							}
							else
							{
								return;
							}
						}
						else
						{
							if (QR1)
							{
								Result = result1.Text;
							}
							else if (QR2)
							{
								Result = result2.Text;
							}
							else
							{
								Result = result3.Text;
							}
						}
						string check = FileTest.QRCheck(Result);
						switch (check)
						{
							case "Future":
								StatusText.text = "Status: That code is for a future version and is unreadable in RS2";
								break;
							case "RS2":
								QRClass QRHeader;
								QRHeader = JsonUtility.FromJson<QRClass>(Result);
								if (QRHeader.Master)
								{
									if (CurrentUUID == "None")
									{
										if (!Devices.Contains(QRHeader.UUID))
										{
											CurrentUUID = QRHeader.UUID;
											TotalQR = QRHeader.MaxFiles;
											ThisQR = 2;
											StatusText.text = "Status: Scan Successful";
										}
										else
										{
											StatusText.text = "Status: Device already scanned";
										}
									}
									else
									{
										if (CurrentUUID == QRHeader.UUID)
										{
											StatusText.text = "Status: Please scan the code " + ThisQR + "/" + (TotalQR + 1);
										}
										else
										{
											StatusText.text = "Status: Please finish scanning the current device";
										}
									}
								}
								else
								{
									if (CurrentUUID == "None")
									{
										if (QRHeader.ThisQR == 1)
										{
											if (!Devices.Contains(QRHeader.UUID))
											{
												if (QRHeader.Compression)
												{
													string CompressedData = StringCompressor.DecompressString(QRHeader.Data);
													Import(CompressedData);
												}
												else
												{
													Import(QRHeader.Data);
												}
												Devices.Add(QRHeader.UUID);
											}
											else
											{
												StatusText.text = "Status: Device already scanned";
											}
										}
										else
										{
											StatusText.text = "Status: Please scan the first QR before scaning the others";
										}
									}
									else
									{
										if (CurrentUUID == QRHeader.UUID)
										{
											if (ThisQR < QRHeader.ThisQR)
											{
												StatusText.text = "Status: Code not ready to be scanned, Please scan the code " + ThisQR + "/" + (TotalQR + 1);
											}
											else if (ThisQR == QRHeader.ThisQR)
											{
												StatusText.text = "Status: Scan Successful";
												if (QRHeader.Compression)
												{
													string CompressedData = StringCompressor.DecompressString(QRHeader.Data);
													DataString += CompressedData;
												}
												else
												{
													DataString += QRHeader.Data;
												}
												ThisQR++;
												if (ThisQR > TotalQR + 1)
												{
													Import(DataString);
													Devices.Add(QRHeader.UUID);
													DataString = "";
												}
											}
											else if (ThisQR > QRHeader.ThisQR)
											{
												StatusText.text = "Status: Code already scanned, Please scan the code " + ThisQR + "/" + (TotalQR + 1);
											}
										}
										else
										{
											StatusText.text = "Status: Please finish scanning the current device";
										}
									}
								}
								break;
							case "RS1.5":
								StatusText.text = "Status: That code is for RS1.5 and is incompatible in RS2";
								break;
							case "RS1":
								StatusText.text = "Status: Scan Sucessful in RS1 Compatibility mode";
								break;
							case "Error":
								StatusText.text = "Status: That code is unreadable";
								break;
						}
						RescanButton.SetActive(true);
						Camera.Stop();
						hasScanned = true;
					}
				}
			}
		}
		else
		{
			if (hasCamera == true)
			{
				Camera.Stop();
				hasCamera = false;
				StartCoroutine(StartCam());
			}
		}
	}

	void Import(string Data)
	{
		Debug.Log(Data);
		FilesQRClass QRData;
		QRData = JsonUtility.FromJson<FilesQRClass>(Data);
		for (int i = 0; i < QRData.FilesCount; i++)
		{
			FilesClass Files;
			int[] Type = QRData.Files[i].QuestionsType;
			string[] Question = QRData.Files[i].Questions;
			string[] Answer = QRData.Files[i].Answers;

			Files = new FilesClass
			{
				SaveVersion = "v1",
				CreatedWith = QRData.Files[i].CreatedWith,
				LastOpenedWith = Application.version,
				Scouter = QRData.Files[i].Scouter,
				QuestionsInt = QRData.Files[i].QuestionsInt,
				QuestionsType = Type,
				Questions = Question,
				Answers = Answer,
				Comment = ""
			};
			string JSON = JsonUtility.ToJson(Files, true);
			byte[] bytes = Encoding.ASCII.GetBytes(JSON);
			File.WriteAllBytes(FilesDataClass.FilePathSaves + "/" + QRData.FilesInfo[i] + ".rs", bytes);
		}

		TotalFiles += QRData.FilesCount;
		DevicesScanned++;
		StatusText.text = "Status: Scan Successful";
		FilesText.text = "Files: " + TotalFiles;
		DeviceText.text = "Devices: " + DevicesScanned;
	}

	public void SetWindow(string Window)
	{
		switch (Window)
		{
			case "Create":
				Scan.SetActive(false);
				ScanTop.SetActive(false);
				Create.SetActive(true);
				CreateTop.SetActive(true);
				Export.SetActive(false);
				ExportTop.SetActive(false);
				break;
			case "Scan":
				Create.SetActive(false);
				CreateTop.SetActive(false);
				Scan.SetActive(true);
				ScanTop.SetActive(true);
				Export.SetActive(false);
				ExportTop.SetActive(false);
				break;
			case "Export":
				Scan.SetActive(false);
				ScanTop.SetActive(false);
				Create.SetActive(false);
				CreateTop.SetActive(false);
				Export.SetActive(true);
				ExportTop.SetActive(true);
				break;
			case "Close":
				Create.SetActive(false);
				CreateTop.SetActive(false);
				Scan.SetActive(false);
				ScanTop.SetActive(false);
				Export.SetActive(false);
				ExportTop.SetActive(false);
				break;
		}

	}

	public List<string> GetFiles()
	{
		List<string> Files = new List<string>();
		string Matching = "*.rs";
		string[] allFiles = Directory.GetFiles(FilesDataClass.FilePathSaves, Matching);
		foreach (string File in allFiles)
		{
			Files.Add(File);
		}
		return Files;
	}
}
