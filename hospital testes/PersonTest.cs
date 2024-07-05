namespace hospital_testes;
using hospital_classes;

[TestClass]
public class PersonTest
{
    [TestMethod]
    public void TestMethod1()
    {
        var person = new Person(fullname: "john doh",
                                  age: 21,
                                   phonenumber: "",
                                   dateofbirth: DateOnly.FromDateTime(DateTime.Today),
                                   gender: "Male",
                                   statue: "Unknown",
                                   address: "Unknown",
                                   bloodtype: "Unknown");

        Assert.AreEqual(21,person.Age);
    }
}