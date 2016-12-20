import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val solver = new Solver(5)
    val actual = solver.solve
    assertResult(3)(actual)
  }
}
