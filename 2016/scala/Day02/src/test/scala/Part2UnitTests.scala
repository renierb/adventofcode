import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2 given scenario") {
    val instructions = Array(
      "ULL",
      "RRDDD",
      "LURDL",
      "UUUUD")

    val solver = new Solver(instructions)
    val actual = solver.solve()
    assertResult(List('5', 'D', 'B', '3'))(actual)
  }
}
