using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer.Factory.Creators
{
    internal class ConcreteInsertSongForm : IFormFactory
    {
        public Form CreateForm()
        {
            return new AudioPlayer.InsertSongForm();
        }
    }
}