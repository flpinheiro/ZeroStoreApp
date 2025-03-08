using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStoreApp.Tests.TestData;

internal class BasicTestData<TClass> where TClass : class
{
    protected Faker<TClass> faker =
    new Faker<TClass>(Locale)
    .StrictMode(true);

    public virtual TClass Build() { return faker; }

    public virtual IEnumerable<TClass> Build(int count) { return faker.Generate(count); }
}
