using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonAlgorithms.Algorithms.Strategy.PredictiveAnalysis
{
    internal class AbraAlg
    {
        internal void Compute()
        {
            IList<SeqmentAssociation> seqments = CreateSegments();

            int maxItemsToPrint = CalculateMaxItemsToPrint(seqments);

            for (int i = 1; i <= maxItemsToPrint; i++)
            {

                PrintItems(BuildAssociations(seqments, maxItemsToPrint, i));
            }

            for (int j = (maxItemsToPrint - 1); j > 0; j--)
            {
                PrintItems(BuildAssociations(seqments, maxItemsToPrint, j));
            }
        }

        private IList<AbraValueAssociation> BuildAssociations(IList<SeqmentAssociation> seqments, int maxItemsToPrint, int currentItemToPrint)
        {
            int currentItemsToPrint = maxItemsToPrint - (maxItemsToPrint - currentItemToPrint);

            IList<AbraValueAssociation> associations = new List<AbraValueAssociation>();

            int itemsLeftToPrint = currentItemsToPrint;

            for (int segment = 0; (segment < seqments.Count && itemsLeftToPrint > 0); segment++)
            {
                var currentSegment = seqments[segment];

                if (segment > 0) { currentItemsToPrint = itemsLeftToPrint; }

                if (itemsLeftToPrint > 0 && currentSegment?.Items.Count > 0)
                {
                    for (int j = 0;
                        (j <= currentItemsToPrint
                        && itemsLeftToPrint > 0
                        && currentSegment.Items.Count > (currentItemsToPrint - itemsLeftToPrint)); j++)
                    {
                        associations.Add(currentSegment.Items[j]);
                        itemsLeftToPrint--;
                    }
                }
            }

            return associations;
        }

        private int CalculateMaxItemsToPrint(IList<SeqmentAssociation> segments)
            => segments.ToList().Sum(x => x.Items.Count);
                


        private IList<SeqmentAssociation> CreateSegments()
            => new List<SeqmentAssociation>()
            {
                new SeqmentAssociation(new List<AbraValueAssociation>
                {
                    AbraValueAssociation.A,
                    AbraValueAssociation.B,
                    AbraValueAssociation.R,
                    AbraValueAssociation.A
                }, 1),
                 new SeqmentAssociation(new List<AbraValueAssociation>
                {
                    AbraValueAssociation.C,
                    AbraValueAssociation.A,
                    AbraValueAssociation.D
                }, 2),
                  new SeqmentAssociation(new List<AbraValueAssociation>
                {
                    AbraValueAssociation.A,
                    AbraValueAssociation.B,
                    AbraValueAssociation.R,
                    AbraValueAssociation.A
                }, 3)

            };

        private void PrintItems(IList<AbraValueAssociation> associations)
        {
           StringBuilder builder = new StringBuilder();

           associations.ToList()
                .ForEach(x => builder.Append(x.ToString()));

            builder.Append($" : {associations.Sum(x => Convert.ToDecimal(x))}");
    

            Console.WriteLine(builder.ToString());
      
         }

      
    }

    internal enum AbraValueAssociation
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        R = 18
    }

    internal class SeqmentAssociation
    {
        internal SeqmentAssociation(IList<AbraValueAssociation> items, int sequenceNumber)
        {

            Items = items;
            SequenceNumber = sequenceNumber;
        }

        internal AbraValueAssociation Key { get; private set; }

        internal IList<AbraValueAssociation> Items { get; private set; }

        internal int SequenceNumber { get; private set; }

        internal int MaxLength
            => Items?.Count ?? 0;
    }
}




