using System;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
	public class PushSettingsPageViewModel : Screen
	{
	    private const string ForumLink = "https://www.zabbix.com/forum/showthread.php?p=136028";

	    private string _token;
	    private bool _isError;

	    public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                NotifyOfPropertyChange(() => Token);
                NotifyOfPropertyChange(() => IsSendToEmail);
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

	    public string Link 
        {
            get { return ForumLink; }
	    }

	    public bool IsBusy
	    {
            get { return !IsError && string.IsNullOrEmpty(Token); }
	    }

	    public bool IsError
	    {
            get { return _isError; }
	        set
	        {
                _isError = value;
                NotifyOfPropertyChange(() => IsError);
                NotifyOfPropertyChange(() => IsBusy);
            }
	    }

	    public void SendToEmail()
	    {
	        var body = string.Format("{0}\n{1}\n\n{2}\n{3}", 
                Localization.AppResources.ZabbixLinkText, Link, 
                Localization.AppResources.PushToken, Token);
            var emailComposeTask = new EmailComposeTask
            {
                Subject = Localization.AppResources.WPDeviceToken, 
                Body = body
            };
            emailComposeTask.Show();
        }

	    public bool IsSendToEmail
	    {
            get { return !string.IsNullOrEmpty(Token); }
	    }

        public void NavigateToForum()
        {
            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri(ForumLink, UriKind.Absolute)
            };
            webBrowserTask.Show();
        }
	}
}