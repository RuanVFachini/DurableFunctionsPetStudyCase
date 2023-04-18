using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsStudyCaseTests
{
    public class DefaultTestCase
    {
        protected static T getFixtureItemWithoutRecursion<T>()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            var fixtureItem = fixture.Create<T>();
            return fixtureItem;
        }
    }
}
