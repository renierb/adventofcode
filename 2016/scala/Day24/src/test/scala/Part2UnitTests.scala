import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2") {
    val input = Vector(
      "###########",
      "#0.1.....2#",
      "#.#######.#",
      "#4.......3#",
      "###########"
    )

    val solver = new Solver(input, 4)
    assertResult((1, 1))(solver.startPos)

    val actual = solver.solve
    assertResult(20)(actual)
  }
}
