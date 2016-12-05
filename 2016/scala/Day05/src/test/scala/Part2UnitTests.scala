import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("abc") {
    val solver = new Solver("abc")
    val actual = solver.solve()
    assertResult("05ace8e3")(actual)
  }
}
