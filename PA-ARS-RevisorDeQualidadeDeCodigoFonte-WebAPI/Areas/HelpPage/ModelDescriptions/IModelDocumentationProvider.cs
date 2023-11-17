using System;
using System.Reflection;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}