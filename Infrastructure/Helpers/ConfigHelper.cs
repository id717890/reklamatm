using System;
using System.Web;
using System.Web.Configuration;

namespace Infrastructure.Helpers
{
    public static class ConfigHelper
    {
        private static string _currentDomain;
        private static int? _currentRegion;

        public static int? GetRegion
        {
            // get region by domen
            //Reklama.tm/lb
            //Reklama.tm/dz
            //Reklama.tm/mr
            //Reklama.tm/ag
            //Reklama.tm/bn
            get
            {
                if (_currentDomain == null)
                {
                    if (HttpContext.Current != null)
                    {
                        _currentDomain = HttpContext.Current.Request.Url.Host.ToLower();
                        switch (_currentDomain)
                        {
                            case "lb.reklama.tm":
                            {
                                _currentRegion = 22;
                                break;
                            }
                            case "dz.reklama.tm":
                            {
                                _currentRegion = 2;
                                break;
                            }
                            case "mr.reklama.tm":
                            {
                                _currentRegion = 17;
                                break;
                            }
                            case "ag.reklama.tm":
                            {
                                _currentRegion = 1;
                                break;
                            }
                            case "bn.reklama.tm":
                            {
                                _currentRegion = 23;
                                break;
                            }
                            case "localhost":
                                _currentDomain = "";
                                string val = WebConfigurationManager.AppSettings.Get("region");
                                if (!String.IsNullOrEmpty(val))
                                {
                                    _currentRegion = int.Parse(val);
                                }
                                break;

                            default:
                            {
                                _currentRegion = null;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _currentDomain = "";
                        string val = WebConfigurationManager.AppSettings.Get("region");
                        if (!String.IsNullOrEmpty(val))
                        {
                            _currentRegion = int.Parse(val);
                        }
                    }
                }

                return _currentRegion;
            }
        }

        public static string GetDomen
        {
            get { return _currentDomain; }
        }

        public static string GetDomainByRegionId(int? domainId)
        {
            string result = null;
            switch (domainId)
            {
                case 22:
                {
                    result = "lb.reklama.tm";
                    break;
                }
                case 2:
                {
                    result = "dz.reklama.tm";
                    break;
                }
                case 17:
                {
                    result = "mr.reklama.tm";
                    break;
                }
                case 1:
                {
                    result = "ag.reklama.tm";
                    break;
                }
                case 23:
                {
                    result = "bn.reklama.tm";
                    break;
                }
                default:
                {

                    if (string.IsNullOrEmpty(_currentDomain))
                    {
                        _currentDomain = HttpContext.Current.Request.Url.Host.ToLower();
                    }
                    if (_currentDomain == "localhost" || _currentDomain == "dev.reklama.tm" ||
                        string.IsNullOrEmpty(_currentDomain))
                    {
                        result = _currentDomain;
                    }
                    else
                    {
                        result = "reklama.tm";
                    }

                    break;
                }

            }
            return result;
        }
    }
}