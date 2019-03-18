
namespace IndentationFixer
{
    public class LineCleanup
    {
        private string _internalString;

        public string InternalString
        {
            get { return _internalString; }
            private set
            {
                _internalString = value;
                IsChanged = true;
            }
        }

        public bool IsChanged { get; private set; }

        public LineCleanup(string internalString)
        {
            _internalString = internalString;
        }

        public void CleanEmptyLines()
        {
            if (string.IsNullOrWhiteSpace(InternalString))
            {
                InternalString = string.Empty;
            }
        }

        public void ReplaceTabToSpaces()
        {
            var indentationSize = 0;
            var hasTabs = false;
            for (int i = 0; i < InternalString.Length; i++)
            {
                var c = InternalString[i];
                if (!(c == ' ' || c == '\t'))
                {
                    break;
                }

                if (c == '\t')
                {
                    hasTabs = true;
                }

                indentationSize = i;
            }

            if (hasTabs)
            {
                var firstSubString = InternalString.Substring(0, indentationSize + 1);
                var secondSubString = InternalString.Substring(indentationSize + 1, InternalString.Length - indentationSize - 1);
                InternalString = string.Concat(firstSubString.Replace("\t", "    "), secondSubString);
                IsChanged = true;
            }
        }

        public void RemoveEndSpaces()
        {
            int end = InternalString.Length - 1;
            while (end >= 0 && char.IsWhiteSpace(InternalString[end]))
            {
                --end;
            }

            if (end != InternalString.Length - 1)
            {
                InternalString = InternalString.Substring(0, end + 1);
            }
        }
    }
}