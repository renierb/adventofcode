import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2") {
    val solver = new Solver(Stream(
      Array("rect", "3x2"),
      Array("rotate", "column", "x=1", "by", "1"),
      Array("rotate", "row", "y=0", "by", "4"),
      Array("rotate", "column", "x=1", "by", "1")
    ), 7, 3)

    val screen = solver.solve
    assertResult(".#..#.#\n#.#....\n.#.....")(solver.printScreen(screen))
  }
}
