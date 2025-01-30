using Classes;

namespace models
{
    public class CustomField
    {

        public CustomField() { }

        public CustomFieldType GetType()
        {
            return new CustomFieldType();
        }

        public string GetModel()
        {
            return "";
        }

    }
}
