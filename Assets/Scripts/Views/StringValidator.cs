using System.Text;

namespace Views
{
    public static class StringValidator
    {
        public static string ValidateInput(string inputText, string defaultText)
        {
            var outputStringBuilder = new StringBuilder();
            var inputStringBuilder = new StringBuilder(inputText);

            var outputText = inputText;
            
            if (inputStringBuilder.Replace(" ", "").Length == 0)
                outputText = outputStringBuilder.Append(defaultText).ToString();

            return outputText;
        }
    }
}