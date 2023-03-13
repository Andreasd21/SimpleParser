using ParserJS.Lexar;

namespace ParserJS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            //string[] AllSource = richTextBox1.Lines;
            string AllSource = richTextBox1.Text;
            Tokinizer tokinizer = new(AllSource);
            List<Token> tokens = tokinizer.MakeTokens();
            //foreach (Token token in tokens)
            //{
            //    MessageBox.Show(token.ToString());
            //}
            try
            {
                Parser parser = new(tokens);
                FormTree TForm = new(parser.Tree);
                TForm.Tform.ShowDialog();
            }
            catch (Exception exe)
            {
                label1.Text = exe.Message;
            }


                
        }

    }
}