﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParserJS
{

    // TO DO:
    // de rechter kant van de branch heeft een te sterke neiging om naar links te gaan,
    // de groter de rechter kant is zullen we zien dat die overlapt met de linker kant.
    public class FormTree
    {
        private bool firstLine = true;
        public Panel panel = new ();
        public Form Tform = new();

        private Size ButtonSize = new(50, 50);
        private Font ButtonFont = new("Arial", 18);
        private int FurthersX = 0;
        
        public FormTree(Tree t)
        {
            Tform.Controls.Add(panel);
            
            Tform.Size = new(2000, 2500);
            panel.Dock = DockStyle.Fill;
            Tform.AutoScroll = true;

            Tform.FormBorderStyle = FormBorderStyle.FixedDialog;
            Tform.MaximizeBox = false;
            Tform.MinimizeBox = false;
            Tform.StartPosition = FormStartPosition.CenterScreen;

            foreach (Branch branch in t.branches)
            {
                AddBranch(branch, FurthersX);
            }
        }
            
        private void AddBranch(Branch b, int x)
        {
            Button Node = CreateButton();
            Button FirstNode = CreateButton();
            Button SecondNode = CreateButton();

            Node.Text = b.BranchValue.Value;
            if (x == 0)
            {
                Node.Left = (Tform.ClientSize.Width - Node.Width) / 2;
            }
            else
            {
                Node.Left = FurthersX + 200;
            }
            panel.Controls.Add(Node);
            FurthersX = Node.Left + Node.Width * 2;

            if (b.First == null)
            {
                return;
            }
            FirstNode.Text = b.First.BranchValue.Value;
            FirstNode.Left = Node.Location.X - ButtonSize.Width * 2;
            FirstNode.Top =  ButtonSize.Height;
            panel.Controls.Add(FirstNode);
            if (b.First.HasChild())
            {         
                HasChild(b.First, FirstNode.Location.X, FirstNode.Location.Y + ButtonSize.Height);
            }

            if (b.Second == null)
            {
                return;
            }
            SecondNode.Text = b.Second.BranchValue.Value;
            SecondNode.Left = Node.Location.X + ButtonSize.Width * 2;
            SecondNode.Top =  ButtonSize.Height;
            panel.Controls.Add(SecondNode);
            FurthersX = SecondNode.Left + SecondNode.Width * 2;
            if (b.Second.HasChild())
            {

                 HasChild(b.Second, SecondNode.Location.X, SecondNode.Location.Y + ButtonSize.Height);

                
            }
        }

        private void HasChild(Branch b, int x, int y)
        {
            Button LeftNode = CreateButton();
            Button RightNode = CreateButton();

            LeftNode.Text = b.First.BranchValue.Value;
            LeftNode.Left = x - ButtonSize.Width;
            LeftNode.Top = y;

            panel.Controls.Add(LeftNode);

            if(b.Second != null)
            {
                RightNode.Text = b.Second.BranchValue.Value;
                RightNode.Left = x + ButtonSize.Width;
                RightNode.Top = y;

                panel.Controls.Add(RightNode);

                FurthersX = RightNode.Left + ButtonSize.Width * 2; 
            }
            if (b.First.HasChild())
            {
                HasChild(b.First, LeftNode.Location.X, LeftNode.Location.Y + ButtonSize.Height);
                if (b.Second.HasChild())
                {
                    HasChild(b.First, RightNode.Location.X, RightNode.Location.Y + ButtonSize.Height);
                }
            }
        }

        private Button CreateButton()
        {
            Button b = new();
            b.Size = ButtonSize;
            b.Font = ButtonFont;
            return b;
        }
    }
}
