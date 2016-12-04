import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1 given scenario") {
    val instructions = Array(
      "ULL",
      "RRDDD",
      "LURDL",
      "UUUUD")

    val solver = new Solver(instructions)
    val actual = solver.solve()
    assertResult(List(1, 9, 8, 5))(actual)
  }
}
