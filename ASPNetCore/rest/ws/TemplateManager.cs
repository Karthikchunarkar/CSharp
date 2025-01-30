namespace rest.ws
{
    public class TemplateManager
    {
        private Dictionary<string, Template> _templates = new Dictionary<string, Template>();

        public bool HasTemplate(string templateId)
        {
            return _templates.ContainsKey(templateId);
        }

        public void AddTemplate(Template template)
        {
            _templates[template.Hash] =  template;
        }

        public Template GetTemplate(string templateId)
        {
            return _templates[templateId];
        }
    }
}
