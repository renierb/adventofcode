import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("abc") {
    val solver = new Solver("abc")
    val actual = solver.solve
    assertResult("18f47a30")(actual)
  }
}
