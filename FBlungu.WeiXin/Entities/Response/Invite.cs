using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Entities.Response
{
    public class Invite
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string jobid { get; set; }
    }
    public class SendInvite
    {       
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string invite_tips { get; set; }
        public callback callback;
        public SendInvite()
        {
            callback = new callback();
        }
    }
    public class callback
    {
        public string url { get; set; }
        public string token { get; set; }
        public string encodingaeskey { get; set; }
    }

    public class Applicationmsg
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public List<Agent> agentlist;
    }

    public class Agent
    {
        public string agentid { get; set; }
        public string name { get; set; }
        public string square_logo_url { get; set; }
        public string round_logo_url { get; set; }
    }
}
