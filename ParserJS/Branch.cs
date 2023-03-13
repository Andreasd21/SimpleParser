using ParserJS.Lexar;

namespace ParserJS
{
    public class Branch
    {
        public Token BranchValue;
        public Branch? First = null;
        public Branch? Second = null;

        public Branch(Token branchValue)
        {
            BranchValue = branchValue;
        }

        public void AddBranchValue(Branch first, Branch second)
        {
            First = first;
            Second = second;
        }

        public bool HasChild()
        {
            if(First != null)
            {
                return true;
            }
            return false;
        }

        public override string? ToString()
        {
            string Branch = $"BranchValue = {BranchValue}{Environment.NewLine}";
            if (First != null)
            {
                Branch += $"\tFirst = {First}{Environment.NewLine}";
            }
            if (Second != null)
            {
                Branch += $"\tSecond = {Second}{Environment.NewLine}";
            }
            return Branch;
        }
    }
}