using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Extensitions
{
    public enum ProductAttributeType
    {
        RadioBox, CheckBox, TextBox, Combobox
    }

    public enum ProductSpecialAttributeType
    {
        Color, Size, Type, Gender, LegthSize,Shaft,Width,Loft,Bounce,Flex,Inseam,Waist
    }

    public enum ProductTypeEnum
    {
        GIAY = 7,
        GAY_GOLF = 5,
        AO = 6
    }
}