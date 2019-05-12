using System;
using System.ComponentModel;
using System.Reflection;

namespace FightSearch.Common.ExtensionMethods
{
	public static class EnumHelper
	{
		public static string Description(this Enum value)
		{
			Type enumType = value.GetType();
			FieldInfo field = enumType.GetField(value.ToString());
			object[] attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute) attributes[0]).Description;
		}
	}
}