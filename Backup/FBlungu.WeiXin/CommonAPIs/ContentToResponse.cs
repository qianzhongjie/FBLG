using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities.GroupSendMsg;
using System.Web.Script.Serialization;
using System.Configuration;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.CommonAPIs
{
    /// <summary>
    /// 将素材json对象转换成群发信息实体
    /// </summary>
    public class ContentToResponse
    {
        static JavaScriptSerializer convertModel = new JavaScriptSerializer();
        /// <summary>
        /// 将素材jsong转换成主动提交实体
        /// </summary>
        /// <param name="scontent"></param>
        /// <returns></returns>
        //public static IGroupSendModeBase ReplyToGroupSendModel(string mediaid, string scontent)
        //{
        //    //ReplyContentModel textContent = convertModel.Deserialize<ReplyContentModel>(scontent);
        //    IGroupSendModeBase ibase = null;
        //    //switch (textContent.types)
        //    //{
        //    //    case "1":
        //    //        GroupSendText model_text = new GroupSendText();
        //    //        model_text.text.content = textContent.text[0].content;
        //    //        ibase = model_text;
        //    //        break;
        //    //    case "2":
        //    //    case "3":
        //    //        GroupSendNews model_news = new GroupSendNews();
        //    //        int newsid = 0;
        //    //        foreach (ReplyContentModel.Newcontent content in textContent.newcontent)
        //    //        {
        //    //            model_news.news.articles.Add(new GroupSendNews.Articles()
        //    //            {

        //    //                description = content.description,
        //    //                picurl = ConfigurationManager.AppSettings["fwq"] + "/MediaFile/" + content.picurl,
        //    //                title = content.title,
        //    //                url = content.url.IndexOf("http") == 0 ? content.url : ConfigurationManager.AppSettings["fwq"] + "/LzljMobile/MobileContents?mediaid=" + mediaid + "&newid=" + newsid
        //    //            });
        //    //            newsid++;
        //    //        }
        //    //        ibase = model_news;
        //    //        break;
        //    //    case "4":
        //    //        GroupSendVoice model_voice = new GroupSendVoice();
        //    //        model_voice.voice.media_id = textContent.voice[0].MediaId;
        //    //        model_voice.furl = textContent.voice[0].MUrl;
        //    //        model_voice.utime = textContent.voice[0].Stime;
        //    //        ibase = model_voice;
        //    //        break;
        //    //    case "5":
        //    //        GroupSendVideo model_video = new GroupSendVideo();
        //    //        model_video.voice.description = textContent.vidoe[0].description;
        //    //        model_video.voice.media_id = textContent.vidoe[0].MediaId;
        //    //        model_video.voice.title = textContent.vidoe[0].title;
        //    //        model_video.furl = textContent.vidoe[0].VUrl;
        //    //        model_video.utime = textContent.vidoe[0].Stime;
        //    //        ibase = model_video;
        //    //        break;
        //    //    case "6":
        //    //        GroupSendFile model_file = new GroupSendFile();
        //    //        model_file.file.media_id = textContent.file[0].MediaId;
        //    //        model_file.furl = textContent.file[0].FUrl;
        //    //        model_file.utime = textContent.file[0].Stime;
        //    //        ibase = model_file;
        //    //        break;
        //    //    case "7":
        //    //        GroupSendImage model_img = new GroupSendImage();
        //    //        model_img.image.media_id = textContent.image[0].MediaId;
        //    //        model_img.furl = textContent.image[0].imageUrl;
        //    //        model_img.utime = textContent.image[0].Stime;
        //    //        ibase = model_img;
        //    //        break;
        //    //}
        //    return ibase;
        //}

        /// <summary>
        /// 将素材jsong转换成被动发送实体
        /// </summary>
        /// <param name="scontent"></param>
        /// <returns></returns>
        public static void ContentConvertResoonse(IRequestMessageBase request, string mediaid, string contment, ref IResponseMessageBase iresponseMessage)
        {
            //try
            //{
            //    ReplyContentModel textContent = convertModel.Deserialize<ReplyContentModel>(contment);
            //    var strongResponseMessagetext = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(request);
            //    var strongResponseMessagenew = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(request);
            //    var strongResponseMessagemusic = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(request);
            //    var strongResponseMessagevoice = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageVoice>(request);
            //    var strongResponseMessagevideo = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageVideo>(request);
            //    switch (textContent.types)
            //    {
            //        case "1":
            //            strongResponseMessagetext.Content = textContent.text[0].content;
            //            iresponseMessage = strongResponseMessagetext;
            //            break;
            //        case "2":
            //        case "3":
            //            int newsid = 0;
            //            foreach (ReplyContentModel.Newcontent content in textContent.newcontent)
            //            {
            //                strongResponseMessagenew.Articles.Add(new Article()
            //                {
            //                    Description = content.description,
            //                    PicUrl = ConfigurationManager.AppSettings["fwq"] + "/MediaFile/" + content.picurl,
            //                    Title = content.title,
            //                    Url = content.url.IndexOf("http") == 0 ? content.url : ConfigurationManager.AppSettings["fwq"] + "/LzljMobile/MobileContents?mediaid=" + mediaid + "&newid=" + newsid
            //                });
            //                newsid++;
            //            }
            //            iresponseMessage = strongResponseMessagenew;
            //            break;
            //        case "4":
            //            if (textContent.voice[0].MediaId != "")
            //            {
            //                strongResponseMessagevoice.Voice.MediaId = textContent.voice[0].MediaId;
            //                iresponseMessage = strongResponseMessagevoice;
            //            }
            //            else
            //            {
            //                strongResponseMessagemusic.Music.Description = textContent.voice[0].description;
            //                strongResponseMessagemusic.Music.HQMusicUrl = textContent.voice[0].MUrl;
            //                strongResponseMessagemusic.Music.MusicUrl = textContent.voice[0].MUrl;
            //                strongResponseMessagemusic.Music.Title = textContent.voice[0].description;
            //                iresponseMessage = strongResponseMessagemusic;
            //            }
            //            break;
            //        case "5":
            //            if (textContent.vidoe[0].MediaId != "")
            //            {
            //                strongResponseMessagevideo.Video.MediaId = textContent.vidoe[0].MediaId;
            //                strongResponseMessagevideo.Video.Description = textContent.vidoe[0].description;
            //                strongResponseMessagevideo.Video.Title = textContent.vidoe[0].title;
            //                iresponseMessage = strongResponseMessagevideo;
            //            }
            //            break;
            //    }
            //}
            //catch { }
        }
    }
}
