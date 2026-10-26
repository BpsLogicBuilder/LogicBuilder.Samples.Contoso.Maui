using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.App.Utils.Interfaces;
using System;

namespace Contoso.XPlatform.Flow
{
    internal static class Constants
    {
        public static readonly Type[] TypeReferences = [typeof(StudentModel), typeof(Student), typeof(ITypeHelper)];
    }
}
