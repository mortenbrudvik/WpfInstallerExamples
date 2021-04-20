using Flurl.Http;
using LanguageExt;
using static LanguageExt.Prelude;

namespace JokeLib
{
    public class ChuckNorris
    {
        public TryAsync<string> TellJoke() =>
            TryAsync<string>(async () =>
                await @"https://api.chucknorris.io/jokes/random"
                    .GetJsonAsync()
                    .Map(x => x.value));
    }
}