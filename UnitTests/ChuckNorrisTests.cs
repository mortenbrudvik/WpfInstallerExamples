using System;
using System.Threading.Tasks;
using FluentAssertions;
using JokeLib;
using Xunit;

namespace UnitTests
{
    public class ChuckNorrisTests
    {
        [Fact]
        public async Task TellJoke()
        {
            var sut = new ChuckNorris();

            var joke = await sut.TellJoke()
                .Match(
                    x => x,
                    ex => "");

            joke.Should().NotBeEmpty();
        }
    }
}