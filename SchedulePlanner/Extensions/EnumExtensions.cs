﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SchedulePlanner.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                           .GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()?
                           .Name ?? enumValue.ToString();
        }
    }
}
