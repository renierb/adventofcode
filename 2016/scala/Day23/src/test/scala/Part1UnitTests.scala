import org.scalatest._
import part1._

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val instructions = Stream(
      "cpy 2 a",
      "tgl a",
      "tgl a",
      "tgl a",
      "cpy 1 a",
      "dec a",
      "dec a"
    )

    val solver = new Solver(instructions)
    val actual = solver.solve
    assertResult(3)(actual)
  }
}