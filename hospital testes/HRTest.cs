namespace hospital_testes;
using hospital_classes;

[TestClass]
public class HRTest
{
    [TestMethod]
    public void HiremethodTest()
    {
        var hr = new HR();
        hr.Hire();

        object Radwa = HR.searchEmployee();

        DateOnly DOB = DateOnly.Parse( Radwa.GetType().GetProperty("DateOfBirth")?.GetValue(Radwa).ToString());
 
        TimeSpan workhours =TimeSpan.Parse(Radwa.GetType().GetProperty("WorkHours")?.GetValue(Radwa)!.ToString()!);
        
        Assert.IsInstanceOfType<Nurse>(Radwa);

        Assert.AreEqual(new DateOnly(2003,1,1),DOB);

        Assert.AreEqual(new TimeSpan(8,0,0),workhours);

        Assert.AreEqual(1,HR.Employees.Count);
    }
}