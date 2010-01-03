/* *******************************************
// LICENSE INFORMATION
// This code is licensed under Apache License 2.0. 
// For more information about the license, please see:
//    http://www.apache.org/licenses/LICENSE-2.0
//
//
// ABOUT THIS PROJECT
//   Owner: Anthony Hand
//   Email: anthony.hand@gmail.com
//   Web Site: http://www.mobileesp.com
//   Source Files: http://code.google.com/p/mobileesp/
//   
//   Versions of this code are available for:
//      PHP, JavaScript, Java, and ASP.NET (C#)
//
// *******************************************
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Subclass this page to inherit the built-in mobile device detection.
/// </summary>
public class MDetectPage : System.Web.UI.Page
{

    private string useragent = "";
    private string httpaccept = "";

    #region Fields - Detection Argument Values

    //standardized values for detection arguments.
    private string dargsIpod = "ipod";
    private string dargsIphone = "iphone";
    private string dargsIphoneOrIpod = "iphoneoripod";
    private string dargsAndroid = "android";
    private string dargsWebKit = "webkit";
    private string dargsSymbianOS = "symbianos";
    private string dargsS60 = "series60";
    private string dargsWindowsMobile = "windowsmobile";
    private string dargsBlackBerry = "blackberry";
    private string dargsPalmOS = "palmos";
    private string dargsPalmWebOS = "webos";
    private string dargsSmartphone = "smartphone";
    private string dargsBrewDevice = "brew";
    private string dargsDangerHiptop = "dangerhiptop";
    private string dargsOperaMobile = "operamobile";
    private string dargsWapWml = "wapwml";
    private string dargsMobileQuick = "mobilequick";
    private string dargsTierIphone = "tieriphone";
    private string dargsTierRichCss = "tierrichcss";
    private string dargsTierOtherPhones = "tierotherphones";

    #endregion Fields - Detection Argument Values

    #region Fields - User Agent Keyword Values

    //Initialize some initial smartphone private string private stringiables.
    private string engineWebKit = "webkit".ToUpper();
    private string deviceAndroid = "android".ToUpper();
    private string deviceIphone = "iphone".ToUpper();
    private string deviceIpod = "ipod".ToUpper();

    private string deviceSymbian = "symbian".ToUpper();
    private string deviceS60 = "series60".ToUpper();
    private string deviceS70 = "series70".ToUpper();
    private string deviceS80 = "series80".ToUpper();
    private string deviceS90 = "series90".ToUpper();

    private string deviceWinMob = "windows ce".ToUpper();
    private string deviceWindows = "windows".ToUpper();
    private string deviceIeMob = "iemobile".ToUpper();
    private string enginePie = "wm5 pie".ToUpper(); //An old Windows Mobile

    private string deviceBB = "blackberry".ToUpper();
    private string vndRIM = "vnd.rim".ToUpper(); //Detectable when BB devices emulate IE or Firefox
    private string deviceBBStorm = "blackberry95".ToUpper(); //Storm 1 and 2
    private string deviceBBBold = "blackberry97".ToUpper(); //Bold
    private string deviceBBTour = "blackberry96".ToUpper(); //Tour
    private string deviceBBCurve = "blackberry89".ToUpper(); //Curve2

    private string devicePalm = "palm".ToUpper();
    private string deviceWebOS = "webos".ToUpper(); //For Palm's new WebOS devices
    private string engineBlazer = "blazer".ToUpper(); //Old Palm
    private string engineXiino = "xiino".ToUpper(); //Another old Palm

    //Initialize private stringiables for mobile-specific content.
    private string vndwap = "vnd.wap".ToUpper();
    private string wml = "wml".ToUpper();

    //Initialize private stringiables for other random devices and mobile browsers.
    private string deviceBrew = "brew".ToUpper();
    private string deviceDanger = "danger".ToUpper();
    private string deviceHiptop = "hiptop".ToUpper();
    private string devicePlaystation = "playstation".ToUpper();
    private string deviceNintendoDs = "nitro".ToUpper();
    private string deviceNintendo = "nintendo".ToUpper();
    private string deviceWii = "wii".ToUpper();
    private string deviceXbox = "xbox".ToUpper();
    private string deviceArchos = "archos".ToUpper();

    private string engineOpera = "opera".ToUpper(); //Popular browser
    private string engineNetfront = "netfront".ToUpper(); //Common embedded OS browser
    private string engineUpBrowser = "up.browser".ToUpper(); //common on some phones
    private string engineOpenWeb = "openweb".ToUpper(); //Transcoding by OpenWave server
    private string deviceMidp = "midp".ToUpper(); //a mobile Java technology
    private string uplink = "up.link".ToUpper();
    private string engineTelecaQ = "teleca q".ToUpper(); //a modern feature phone browser

    private string devicePda = "pda".ToUpper(); //some devices report themselves as PDAs
    private string mini = "mini".ToUpper();  //Some mobile browsers put "mini" in their names.
    private string mobile = "mobile".ToUpper(); //Some mobile browsers put "mobile" in their user agent private strings.
    private string mobi = "mobi".ToUpper(); //Some mobile browsers put "mobi" in their user agent private strings.

    //Use Maemo, Tablet, and Linux to test for Nokia"s Internet Tablets.
    private string maemo = "maemo".ToUpper();
    private string maemoTablet = "tablet".ToUpper();
    private string linux = "linux".ToUpper();
    private string qtembedded = "qt embedded".ToUpper(); //for Sony Mylo
    private string mylocom2 = "com2".ToUpper(); //for Sony Mylo also

    //In some UserAgents, the only clue is the manufacturer.
    private string manuSonyEricsson = "sonyericsson".ToUpper();
    private string manuericsson = "ericsson".ToUpper();
    private string manuSamsung1 = "sec-sgh".ToUpper();
    private string manuSony = "sony".ToUpper();

    //In some UserAgents, the only clue is the operator.
    private string svcDocomo = "docomo".ToUpper();
    private string svcKddi = "kddi".ToUpper();
    private string svcVodafone = "vodafone".ToUpper();

    #endregion Fields - User Agent Keyword Values

    /// <summary>
    /// To instantiate a WebPage sub-class with built-in
    /// mobile device detection delegates and events.
    /// </summary>
    public MDetectPage()
    {

    }

    /// <summary>
    /// To run the device detection methods andd fire 
    /// any existing OnDetectXXX events. 
    /// </summary>
    public void FireEvents()
    {
        if (useragent == "" && httpaccept == "")
        {
            useragent = Request.ServerVariables["HTTP_USER_AGENT"].ToUpper();
            httpaccept = Request.ServerVariables["HTTP_ACCEPT"].ToUpper();

        }

        #region Event Fire Methods

        MDetectArgs mda = null;
        if (this.DetectIpod())
        {
            mda = new MDetectArgs(dargsIpod);
            if (this.OnDetectIpod != null)
            {
                this.OnDetectIpod(this, mda);
            }
        }
        else if (this.DetectIphone())
        {
            mda = new MDetectArgs(dargsIphone);
            if (this.OnDetectIphone != null)
            {
                this.OnDetectIphone(this, mda);
            }
        }
        else if (this.DetectIphoneOrIpod())
        {
            mda = new MDetectArgs(dargsIphoneOrIpod);
            if (this.OnDetectDetectIPhoneOrIpod != null)
            {
                this.OnDetectDetectIPhoneOrIpod(this, mda);
            }
        }
        else if (this.DetectAndroid())
        {
            mda = new MDetectArgs(dargsAndroid);
            if (this.OnDetectAndroid != null)
            {
                this.OnDetectAndroid(this, mda);
            }
        }
        else if (this.DetectWebkit())
        {
            mda = new MDetectArgs(dargsWebKit);
            if (this.OnDetectWebkit != null)
            {
                this.OnDetectWebkit(this, mda);
            }
        }
        else if (this.DetectS60OssBrowser())
        {
            mda = new MDetectArgs(dargsS60);
            if (this.OnDetectS60OssBrowser != null)
            {
                this.OnDetectS60OssBrowser(this, mda);
            }
        }
        else if (this.DetectSymbianOS())
        {
            mda = new MDetectArgs(dargsSymbianOS);
            if (this.OnDetectSymbianOS != null)
            {
                this.OnDetectSymbianOS(this, mda);
            }
        }
        else if (this.DetectWindowsMobile())
        {
            mda = new MDetectArgs(dargsWindowsMobile);
            if (this.OnDetectWindowsMobile != null)
            {
                this.OnDetectWindowsMobile(this, mda);
            }
        }
        else if (this.DetectBlackBerry())
        {
            mda = new MDetectArgs(dargsBlackBerry);
            if (this.OnDetectBlackBerry != null)
            {
                this.OnDetectBlackBerry(this, mda);
            }
        }
        else if (this.DetectPalmOS())
        {
            mda = new MDetectArgs(dargsPalmOS);
            if (this.OnDetectPalmOS != null)
            {
                this.OnDetectPalmOS(this, mda);
            }
        }
        else if (this.DetectPalmWebOS())
        {
            mda = new MDetectArgs(dargsPalmWebOS);
            if (this.OnDetectPalmWebOS != null)
            {
                this.OnDetectPalmWebOS(this, mda);
            }
        }
        else if (this.DetectSmartphone())
        {
            mda = new MDetectArgs(dargsSmartphone);
            if (this.OnDetectSmartphone != null)
            {
                this.OnDetectSmartphone(this, mda);
            }
        }
        else if (this.DetectBrewDevice())
        {
            mda = new MDetectArgs(dargsBrewDevice);
            if (this.OnDetectBrewDevice != null)
            {
                this.OnDetectBrewDevice(this, mda);
            }
        }
        else if (this.DetectDangerHiptop())
        {
            mda = new MDetectArgs(dargsDangerHiptop);
            if (this.OnDetectDangerHiptop != null)
            {
                this.OnDetectDangerHiptop(this, mda);
            }
        }
        else if (this.DetectOperaMobile())
        {
            mda = new MDetectArgs(dargsOperaMobile);
            if (this.OnDetectOperaMobile != null)
            {
                this.OnDetectOperaMobile(this, mda);
            }
        }
        else if (this.DetectWapWml())
        {
            mda = new MDetectArgs(dargsWapWml);
            if (this.OnDetectWapWml != null)
            {
                this.OnDetectWapWml(this, mda);
            }
        }
        else if (this.DetectMobileQuick())
        {
            mda = new MDetectArgs(dargsMobileQuick);
            if (this.OnDetectMobileQuick != null)
            {
                this.OnDetectMobileQuick(this, mda);
            }
        }
        else if (this.DetectTierIphone())
        {
            mda = new MDetectArgs(dargsTierIphone);
            if (this.OnDetectTierIphone != null)
            {
                this.OnDetectTierIphone(this, mda);
            }
        }
        else if (this.DetectTierRichCss())
        {
            mda = new MDetectArgs(dargsTierRichCss);
            if (this.OnDetectTierRichCss != null)
            {
                this.OnDetectTierRichCss(this, mda);
            }
        }
        else if (this.DetectTierOtherPhones())
        {
            mda = new MDetectArgs(dargsTierOtherPhones);
            if (this.OnDetectTierOtherPhones != null)
            {
                this.OnDetectTierOtherPhones(this, mda);
            }
        }

        #endregion Event Fire Methods

    }

    public class MDetectArgs : EventArgs
    {
        public MDetectArgs(string type)
        {
            this.Type = type;
        }

        public readonly string Type;
    }

    #region Mobile Device Detection Methods 

    //**************************
    // Detects if the current device is an iPod Touch.
    public bool DetectIpod()
    {
        if (useragent.IndexOf(deviceIpod)!= -1)
            return true;
        else
            return false;
    }

    //Ipod delegate
    public delegate void DetectIpodHandler(object page, MDetectArgs args);
    public event DetectIpodHandler OnDetectIpod;


    //**************************
    // Detects if the current device is an iPhone.
    public bool DetectIphone()
    {
        if (useragent.IndexOf(deviceIphone)!= -1)
        {
            //The iPod touch says it's an iPhone! So let's disambiguate.
            if (DetectIpod())
            {
                return false;
            }
            else
                return true;
        }
        else
            return false;
    }
    //IPhone delegate
    public delegate void DetectIphoneHandler(object page, MDetectArgs args);
    public event DetectIphoneHandler OnDetectIphone;

    //**************************
    // Detects if the current device is an iPhone or iPod Touch.
    public bool DetectIphoneOrIpod()
    {
        //We repeat the searches here because some iPods may report themselves as an iPhone, which would be okay.
        if (useragent.IndexOf(deviceIphone)!= -1 ||
            useragent.IndexOf(deviceIpod)!= -1)
            return true;
        else
            return false;
    }
    //IPhoneOrIpod delegate
    public delegate void DetectIPhoneOrIpodHandler(object page, MDetectArgs args);
    public event DetectIPhoneOrIpodHandler OnDetectDetectIPhoneOrIpod;

    //**************************
    // Detects if the current device is an Android OS-based device.
    public bool DetectAndroid()
    {
        if (useragent.IndexOf(deviceAndroid)!= -1)
            return true;
        else
            return false;
    }
    //Android delegate
    public delegate void DetectAndroidHandler(object page, MDetectArgs args);
    public event DetectAndroidHandler OnDetectAndroid;


    //**************************
    // Detects if the current device is an Android OS-based device and
    //   the browser is based on WebKit.
    public bool DetectAndroidWebKit()
    {
        if (DetectAndroid())
        {
            if (DetectWebkit())
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    //**************************
    // Detects if the current browser is based on WebKit.
    public bool DetectWebkit()
    {
        if (useragent.IndexOf(engineWebKit)!= -1)
            return true;
        else
            return false;
    }

    //Webkit delegate
    public delegate void DetectWebkitHandler(object page, MDetectArgs args);
    public event DetectWebkitHandler OnDetectWebkit;

    //**************************
    // Detects if the current browser is the Nokia S60 Open Source Browser.
    public bool DetectS60OssBrowser()
    {
        //First, test for WebKit, then make sure it's either Symbian or S60.
        if (DetectWebkit())
        {
            if (useragent.IndexOf(deviceSymbian)!= -1 ||
                useragent.IndexOf(deviceS60)!= -1)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    //S60OssBrowser delegate
    public delegate void DetectS60OssBrowserHandler(object page, MDetectArgs args);
    public event DetectS60OssBrowserHandler OnDetectS60OssBrowser;

    //**************************
    // Detects if the current device is any Symbian OS-based device,
    //   including older S60, Series 70, Series 80, Series 90, and UIQ, 
    //   or other browsers running on these devices.
    public bool DetectSymbianOS()
    {
        if (useragent.IndexOf(deviceSymbian)!= -1 ||
            useragent.IndexOf(deviceS60)!= -1 ||
            useragent.IndexOf(deviceS70)!= -1 ||
            useragent.IndexOf(deviceS80)!= -1 ||
            useragent.IndexOf(deviceS90)!= -1)
            return true;
        else
            return false;
    }

    //SymbianOS delegate
    public delegate void DetectSymbianOSHandler(object page, MDetectArgs args);
    public event DetectSymbianOSHandler OnDetectSymbianOS;

    //**************************
    // Detects if the current browser is a Windows Mobile device.
    public bool DetectWindowsMobile()
    {
        //Most devices use 'Windows CE', but some report 'iemobile' 
        //  and some older ones report as 'PIE' for Pocket IE. 
        if (useragent.IndexOf(deviceWinMob)!= -1 ||
            useragent.IndexOf(deviceIeMob)!= -1 ||
            useragent.IndexOf(enginePie)!= -1)
            return true;
        if (DetectWapWml() == true &&
            useragent.IndexOf(deviceWindows)!= -1)
            return true;
        else
            return false;
    }

    //WindowsMobile delegate
    public delegate void DetectWindowsMobileHandler(object page, MDetectArgs args);
    public event DetectWindowsMobileHandler OnDetectWindowsMobile;

    //**************************
    // Detects if the current browser is a BlackBerry of some sort.
    public bool DetectBlackBerry()
    {
        if (useragent.IndexOf(deviceBB)!= -1)
            return true;
        if (httpaccept.IndexOf(vndRIM)!= -1)
            return true;
        else
            return false;
    }
    //BlackBerry delegate
    public delegate void DetectBlackBerryHandler(object page, MDetectArgs args);
    public event DetectBlackBerryHandler OnDetectBlackBerry;


    //**************************
    // Detects if the current browser is a BlackBerry Touch
    //    device, such as the Storm.
    public bool DetectBlackBerryTouch()
    {
        if (useragent.IndexOf(deviceBBStorm) != -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current browser is a BlackBerry device AND
    //    has a more capable recent browser. 
    //    Examples, Storm, Bold, Tour, Curve2
    public bool DetectBlackBerryHigh()
    {
        if (DetectBlackBerry())
        {
            if (DetectBlackBerryTouch() ||
                useragent.IndexOf(deviceBBBold) != -1 ||
                useragent.IndexOf(deviceBBTour) != -1 ||
                useragent.IndexOf(deviceBBCurve) != -1)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    //**************************
    // Detects if the current browser is a BlackBerry device AND
    //    has an older, less capable browser. 
    //    Examples: Pearl, 8800, Curve1.
    public bool DetectBlackBerryLow()
    {
        if (DetectBlackBerry())
        {
            //Assume that if it's not in the High tier, then it's Low.
            if (DetectBlackBerryHigh())
                return false;
            else
                return true;
        }
        else
            return false;
    }

    //**************************
    // Detects if the current browser is on a PalmOS device.
    public bool DetectPalmOS()
    {
        //Most devices nowadays report as 'Palm', but some older ones reported as Blazer or Xiino.
        if (useragent.IndexOf(devicePalm) != -1 ||
            useragent.IndexOf(engineBlazer) != -1 ||
            useragent.IndexOf(engineXiino) != -1)
        {
            //Make sure it's not WebOS first
            if (DetectPalmWebOS() == true)
                return false;
            else
                return true;
        }
        else
            return false;
    }
    //PalmOS delegate
    public delegate void DetectPalmOSHandler(object page, MDetectArgs args);
    public event DetectPalmOSHandler OnDetectPalmOS;


    //**************************
    // Detects if the current browser is on a Palm device
    //    running the new WebOS.
    public bool DetectPalmWebOS()
    {
        if (useragent.IndexOf(deviceWebOS))
            return true;
        else
            return false;
    }

    //PalmWebOS delegate
    public delegate void DetectPalmWebOSHandler(object page, MDetectArgs args);
    public event DetectPalmWebOSHandler OnDetectPalmWebOS;


    //**************************
    // Detects if the current browser is a
    //    Garmin Nuvifone.
    public bool DetectGarminNuvifone()
    {
        if (useragent.IndexOf(deviceNuvifone))
            return true;
        else
            return false;
    }


    //**************************
    // Check to see whether the device is any device
    //   in the 'smartphone' category.
    public bool DetectSmartphone()
    {
        if (DetectIphoneOrIpod() == true)
            return true;
        if (DetectS60OssBrowser() == true)
            return true;
        if (DetectSymbianOS() == true)
            return true;
        if (DetectAndroid() == true)
            return true;
        if (DetectWindowsMobile() == true)
            return true;
        if (DetectBlackBerry() == true)
            return true;
        if (DetectPalmWebOS() == true)
            return true;
        if (DetectPalmOS() == true)
            return true;
        if (DetectGarminNuvifone() == true)
            return true;
        else
            return false;
    }

    //DetectSmartphone delegate
    public delegate void DetectSmartphoneHandler(object page, MDetectArgs args);
    public event DetectSmartphoneHandler OnDetectSmartphone;


    //**************************
    // Detects whether the device is a Brew-powered device.
    public bool DetectBrewDevice()
    {
        if (useragent.IndexOf(deviceBrew)!= -1)
            return true;
        else
            return false;
    }

    //BrewDevice delegate
    public delegate void DetectBrewDeviceHandler(object page, MDetectArgs args);
    public event DetectBrewDeviceHandler OnDetectBrewDevice;

    //**************************
    // Detects the Danger Hiptop device.
    public bool DetectDangerHiptop()
    {
        if (useragent.IndexOf(deviceDanger)!= -1 ||
            useragent.IndexOf(deviceHiptop)!= -1)
            return true;
        else
            return false;
    }
    //DangerHiptop delegate
    public delegate void DetectDangerHiptopHandler(object page, MDetectArgs args);
    public event DetectDangerHiptopHandler OnDetectDangerHiptop;

    //**************************
    // Detects if the current browser is Opera Mobile or Mini.
    public bool DetectOperaMobile()
    {
        if (useragent.IndexOf(engineOpera)!= -1)
        {
            if ((useragent.IndexOf(mini)!= -1) ||
             (useragent.IndexOf(mobi)!= -1))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    //DangerHiptop delegate
    public delegate void DetectOperaMobileHandler(object page, MDetectArgs args);
    public event DetectOperaMobileHandler OnDetectOperaMobile;


    //**************************
    // Detects whether the device supports WAP or WML.
    public bool DetectWapWml()
    {
        if (httpaccept.IndexOf(vndwap)!= -1 ||
            httpaccept.IndexOf(wml)!= -1)
            return true;
        else
            return false;
    }
    //WapWml delegate
    public delegate void DetectWapWmlHandler(object page, MDetectArgs args);
    public event DetectWapWmlHandler OnDetectWapWml;

    //**************************
    // The quick way to detect for a mobile device.
    //   Will probably detect most recent/current mid-tier Feature Phones
    //   as well as smartphone-class devices.
    public bool DetectMobileQuick()
    {
        //Most mobile browsing is done on smartphones
        if (DetectSmartphone() == true)
            return true;

        if (DetectWapWml() == true)
            return true;
        if (DetectBrewDevice() == true)
            return true;
        if (DetectOperaMobile() == true)
            return true;

        if (useragent.IndexOf(engineNetfront) != -1)
            return true;
        if (useragent.IndexOf(engineUpBrowser)!= -1)
            return true;
        if (useragent.IndexOf(engineOpenWeb)!= -1)
            return true;

        if (DetectDangerHiptop() == true)
            return true;

        if (DetectMidpCapable() == true)
            return true;

        if (DetectMaemoTablet() == true)
            return true;
        if (DetectArchos() == true)
            return true;

        if (useragent.IndexOf(devicePda) != -1)
            return true;
        if (useragent.IndexOf(mobile)!= -1)
            return true;

        else
            return false;
    }

    //DetectMobileQuick delegate
    public delegate void DetectMobileQuickHandler(object page, MDetectArgs args);
    public event DetectMobileQuickHandler OnDetectMobileQuick;


    //**************************
    // Detects if the current device is a Sony Playstation.
    public bool DetectSonyPlaystation()
    {
        if (useragent.IndexOf(devicePlaystation)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device is a Nintendo game device.
    public bool DetectNintendo()
    {
        if (useragent.IndexOf(deviceNintendo)!= -1 ||
             useragent.IndexOf(deviceWii)!= -1 ||
             useragent.IndexOf(deviceNintendoDs)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device is a Microsoft Xbox.
    public bool DetectXbox()
    {
        if (useragent.IndexOf(deviceXbox)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device is an Internet-capable game console.
    public bool DetectGameConsole()
    {
        if (DetectSonyPlaystation() == true)
            return true;
        else if (DetectNintendo() == true)
            return true;
        else if (DetectXbox() == true)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device supports MIDP, a mobile Java technology.
    public bool DetectMidpCapable()
    {
        if (useragent.IndexOf(deviceMidp)!= -1 ||
            httpaccept.IndexOf(deviceMidp)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device is on one of the Maemo-based Nokia Internet Tablets.
    public bool DetectMaemoTablet()
    {
        if (useragent.IndexOf(maemo)!= -1)
            return true;
        //Must be Linux + Tablet, or else it could be something else. 
        else if (useragent.IndexOf(maemoTablet)!= -1 &&
            useragent.IndexOf(linux)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current device is an Archos media player/Internet tablet.
    public bool DetectArchos()
    {
        if (useragent.IndexOf(deviceArchos)!= -1)
            return true;
        else
            return false;
    }

    //**************************
    // Detects if the current browser is a Sony Mylo device.
    public bool DetectSonyMylo()
    {
        if (useragent.IndexOf(manuSony)!= -1)
        {
            if ((useragent.IndexOf(qtembedded)!= -1) ||
             (useragent.IndexOf(mylocom2)!= -1))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    //**************************
    // The longer and more thorough way to detect for a mobile device.
    //   Will probably detect most feature phones,
    //   smartphone-class devices, Internet Tablets, 
    //   Internet-enabled game consoles, etc.
    //   This ought to catch a lot of the more obscure and older devices, also --
    //   but no promises on thoroughness!
    public bool DetectMobileLong()
    {
        if (DetectMobileQuick() == true)
            return true;
        if (DetectGameConsole() == true)
            return true;
        if (DetectSonyMylo() == true)
            return true;

        //Detect older phones from certain manufacturers and operators. 
        if (useragent.IndexOf(uplink) != -1)
            return true;
        if (useragent.IndexOf(manuSonyEricsson) != -1)
            return true;
        if (useragent.IndexOf(manuericsson) != -1)
            return true;
        if (useragent.IndexOf(manuSamsung1) != -1)
            return true;

        if (useragent.IndexOf(svcDocomo) != -1)
            return true;
        if (useragent.IndexOf(svcKddi) != -1)
            return true;
        if (useragent.IndexOf(svcVodafone) != -1)
            return true;

        else
            return false;
    }



    //*****************************
    // For Mobile Web Site Design
    //*****************************


    //**************************
    // The quick way to detect for a tier of devices.
    //   This method detects for devices which can 
    //   display iPhone-optimized web content.
    //   Includes iPhone, iPod Touch, Android, etc.
    public bool DetectTierIphone()
    {
        if (DetectIphoneOrIpod() == true)
            return true;
        if (DetectAndroid() == true)
            return true;
        if (DetectAndroidWebKit() == true)
            return true;
        if (DetectPalmWebOS() == true)
            return true;
        if (DetectGarminNuvifone() == true)
            return true;
        if (DetectMaemoTablet() == true)
            return true;
        else
            return false;
    }

    //DetectTierIphone delegate
    public delegate void DetectTierIphoneHandler(object page, MDetectArgs args);
    public event DetectTierIphoneHandler OnDetectTierIphone;


    //**************************
    // The quick way to detect for a tier of devices.
    //   This method detects for devices which are likely to be capable 
    //   of viewing CSS content optimized for the iPhone, 
    //   but may not necessarily support JavaScript.
    //   Excludes all iPhone Tier devices.
    public bool DetectTierRichCss()
    {
        if (DetectMobileQuick() == true)
        {
            if (DetectTierIphone() == true)
                return false;

            if (DetectWebkit() == true)
                return true;
            if (DetectS60OssBrowser() == true)
                return true;

            //Note: 'High' BlackBerry devices ONLY
            if (DetectBlackBerryHigh() == true)
                return true;

            if (DetectWindowsMobile() == true)
                return true;
            if (useragent.IndexOf(engineTelecaQ) != -1)
                return true;

            else
                return false;
        }
        else
            return false;
    }

    //DetectTierRichCss delegate
    public delegate void DetectTierRichCssHandler(object page, MDetectArgs args);
    public event DetectTierRichCssHandler OnDetectTierRichCss;


    //**************************
    // The quick way to detect for a tier of devices.
    //   This method detects for all other types of phones,
    //   but excludes the iPhone and Smartphone Tier devices.
    public bool DetectTierOtherPhones()
    {
        if (DetectMobileQuick() == true)
        {
            if (DetectTierIphone() == true)
                return false;
            if (DetectTierSmartphones() == true)
                return false;
            else
                return true;
        }
        else
            return false;
    }

    //DetectTierOtherPhones delegate
    public delegate void DetectTierOtherPhonesHandler(object page, MDetectArgs args);
    public event DetectTierOtherPhonesHandler OnDetectTierOtherPhones;

    //***************************************************************
    #endregion

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        useragent = Request.ServerVariables["HTTP_USER_AGENT"].ToUpper();
        httpaccept = Request.ServerVariables["HTTP_ACCEPT"].ToUpper();

    }
}
