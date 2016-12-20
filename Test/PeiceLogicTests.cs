using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    
    public class PeiceLogicTests
    {
        private object[] PawnPassTestData()
        {
            return new object[]
            {
            new object[]
            {
                new Peice()
                {
                    Side = Side.White,
                    Type = PeiceType.Pawn
                },
                0,
                1
            },
            new object[]
            {
                new Peice()
                {
                    Side = Side.Black,
                    Type = PeiceType.Pawn
                },
                0,
                -1
            }
            };
        }

        private static object[] PawnFailTestData()
        {
            return new object[]
            {
            new object[]
            {
                new Peice()
                {
                    Side = Side.White,
                    Type = PeiceType.Pawn
                },
                0,
                1
            },
            new object[]
            {
                new Peice()
                {
                    Side = Side.Black,
                    Type = PeiceType.Pawn
                },
                0,
                -1
            }
            };
        }

        [Theory]
        [MemberData(nameof(PawnPassTestData))]
        public void PawnPass(Peice pawn, int dX, int dY)
        {

        }

    }
}
