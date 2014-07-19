using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Serialization is the process of converting an object into a stream of bytes in order to store the object or transmit it to memory, a database, or a file. 
        //Its main purpose is to save the state of an object in order to be able to recreate it when needed.
        [Serializable]// allows the class to be saved in file
        public class data
        {
        public string name;
        public string surname;
        public string city;
        public string number;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid.EndEdit(); // from System.Windows.Forms
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();// from System.Windows.Forms
            saveFileDialog1.RestoreDirectory = true;

            //read and filter the raw data
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter(); //Serializes and deserializes an object, or an entire graph of connected objects, in binary format.
                FileStream output = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);

                int n = Grid.RowCount;
                data[] Person = new data[n - 1];//We have as many records as many rows, rows are added automaticly so we have always one row more than we need, so n is a number of rows -1 empty row
                for (int i = 0; i < n - 1; i++)
                {
                    Person[i] = new data();
                    //GRID has two numbers in"[]" first number is an index of column, second is a an index of row', indexing always starts from 0'
                    Person[i].name = Grid[0, i].Value.ToString();
                    Person[i].surname = Grid[1, i].Value.ToString();
                    Person[i].city = Grid[2, i].Value.ToString();
                    Person[i].number = Grid[3, i].Value.ToString();
                }

                formatter.Serialize(output, Person);
                output.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reading a file and adding data to Grid
            openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter reader = new BinaryFormatter();
                FileStream input = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                data[] Person = (data[])reader.Deserialize(input);
                Grid.Rows.Clear();

                for (int i = 0; i < Person.Length; i++)
                {
                    Grid.Rows.Add();
                    Grid[0, i].Value = Person[i].name;
                    Grid[1, i].Value = Person[i].surname;
                    Grid[2, i].Value = Person[i].city;
                    Grid[3, i].Value = Person[i].number;
                }



            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
