using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    public GameObject Manager;
    public GameObject NewsWindow;
    public GameObject News1;
    public GameObject News2;
    public GameObject News3;
    public GameObject News4;
    public GameObject News5;
    public Text Title1;
    public Text Title2;
    public Text Title3;
    public Text Title4;
    public Text Title5;
    public Text Desc1;
    public Text Desc2;
    public Text Desc3;
    public Text Desc4;
    public Text Desc5;
    public string Link1;
    public string Link2;
    public string Link3;
    public string Link4;
    public string Link5;
    AWClass News;
    float OffsetX;
    float OffsetY;

    public void Wake()
    {
        OffsetX = ResolutionManager.ScreenOffsetW;
        OffsetY = ResolutionManager.ScreenOffsetH;
        float NewsX = NewsWindow.transform.position.x;
        NewsWindow.transform.position = new Vector2(NewsX, 1421.25f * OffsetY);
        News1.SetActive(false);
        News2.SetActive(false);
        News3.SetActive(false);
        News4.SetActive(false);
        News5.SetActive(false);
    }

    public void SetNews(AWClass AWNews)
    {

        News = AWNews;
        Title1.text = News.News1.Title;
        Title2.text = News.News2.Title;
        Title3.text = News.News3.Title;
        Title4.text = News.News4.Title;
        Title5.text = News.News5.Title;
        Desc1.text = News.News1.Desc;
        Desc2.text = News.News2.Desc;
        Desc3.text = News.News3.Desc;
        Desc4.text = News.News4.Desc;
        Desc5.text = News.News5.Desc;
        Link1 = News.News1.Link;
        Link2 = News.News2.Link;
        Link3 = News.News3.Link;
        Link4 = News.News4.Link;
        Link5 = News.News5.Link;
        if (News.News1.Data[0].ToString() == "1")
        {
            News1.SetActive(true);
        }
        if (News.News2.Data[0].ToString() == "1")
        {
            News2.SetActive(true);
        }
        if (News.News3.Data[0].ToString() == "1")
        {
            News3.SetActive(true);
        }
        if (News.News4.Data[0].ToString() == "1")
        {
            News4.SetActive(true);
        }
        if (News.News5.Data[0].ToString() == "1")
        {
            News5.SetActive(true);
        }
        float NewsX = NewsWindow.transform.position.x;
        if (News5.activeSelf)
        {
            NewsWindow.transform.position = new Vector2(NewsX, 638.75f * OffsetY); 
        }
        else if (News4.activeSelf)
        {
            NewsWindow.transform.position = new Vector2(NewsX, 766.25f * OffsetY);
        }
        else if(News3.activeSelf)
        {
            NewsWindow.transform.position = new Vector2(NewsX, 891.25f * OffsetY);
        }
        else if (News2.activeSelf)
        {
            NewsWindow.transform.position = new Vector2(NewsX, 1016.25f * OffsetY);
        }
        else if (News1.activeSelf)
        {
            NewsWindow.transform.position = new Vector2(NewsX, 1141.25f * OffsetY);
        }
        else
        {
            NewsWindow.transform.position = new Vector2(NewsX, 1421.25f * OffsetY);
        }
    }

    public void Button1()
    {
        if (News.News1.Data[1].ToString() == "1")
        {
            Manager.GetComponent<ExternalLinks>().ExternalLink(Link1);
        }
    }

    public void Button2()
    {
        if (News.News2.Data[1].ToString() == "1")
        {
            Manager.GetComponent<ExternalLinks>().ExternalLink(Link2);
        }
    }

    public void Button3()
    {
        if (News.News3.Data[1].ToString() == "1")
        {
            Manager.GetComponent<ExternalLinks>().ExternalLink(Link3);
        }
    }

    public void Button4()
    {
        if (News.News4.Data[1].ToString() == "1")
        {
            Manager.GetComponent<ExternalLinks>().ExternalLink(Link4);
        }
    }

    public void Button5()
    {
        if (News.News5.Data[1].ToString() == "1")
        {
            Manager.GetComponent<ExternalLinks>().ExternalLink(Link5);
        }
    }
}
