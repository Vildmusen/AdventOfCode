using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day8
    {
        string[] input;
        List<int> numbers = new List<int>();
        List<List<int>> layers = new List<List<int>>();
        int[] image = new int[150];

        public void Start()
        {
            Setup();
            GetLayers(25, 6);
            Console.WriteLine(GetBestLayer());
            BuildMessage();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    string tecken = image[j + (i * 25)] == 1 ? "#" : ".";
                    Console.Write(tecken);
                }
                Console.WriteLine();
            }
        }

        private void BuildMessage()
        {
            for (int i = 0; i < image.Length; i++)
            {
                for (int j = 0; j < layers.Count; j++)
                {
                    if(layers[j][i] != 2)
                    {
                        image[i] = layers[j][i];
                        break;
                    }
                }
            }
        }

        private int GetBestLayer()
        {
            int min = int.MaxValue;
            int value = 0;
            foreach(List<int> layer in layers)
            {
                int zeros = GetAppearanceOf(layer, 0);
                if (zeros < min)
                {
                    min = zeros;
                    value = GetAppearanceOf(layer, 1) * GetAppearanceOf(layer, 2);
                }
            }
            return value;
        }

        private int GetAppearanceOf(List<int> layer, int value)
        {
            int count = 0;
            foreach(int i in layer)
            {
                if(i == value)
                {
                    count++;
                }
            }
            return count;
        }

        private void GetLayers(int width, int height)
        {
            int i = 0;
            int size = width * height;
            while (i < numbers.Count)
            {
                List<int> layer = new List<int>();
                foreach(int part in numbers.GetRange(i, size))
                {
                    layer.Add(part);
                }
                layers.Add(layer);
                i += size;
            }
        }

        private void Setup()
        {
            input = Utils.ReadFromFile("8");
            string data = input[0];
            foreach (char n in data)
            {
                numbers.Add(Int32.Parse(n.ToString()));
            }
        }
    }
}
