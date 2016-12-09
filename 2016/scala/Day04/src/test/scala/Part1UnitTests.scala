import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val solver = new Solver(Stream(
      "aaaaa-bbb-z-y-x-123[abxyz]",
      "a-b-c-d-e-f-g-h-987[abcde]",
      "not-a-real-room-404[oarel]",
      "totally-real-room-200[decoy]"
    ))

    val actual = solver.solve
    assertResult(1514)(actual)
  }
}