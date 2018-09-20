using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunMall.Web.Models
{

    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class GenerateIdAttribute : System.Attribute
    {
    }
}