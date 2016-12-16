import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1: 1 becomes 100") {
    val solver = new Solver("1", 3)
    val actual = solver.transform()
    assertResult("100")(actual)
  }

  test("part 1: 0 becomes 001") {
    val solver = new Solver("0", 3)
    val actual = solver.transform()
    assertResult("001")(actual)
  }

  test("part 1: 11111 becomes 11111000000") {
    val solver = new Solver("11111", 11)
    val actual = solver.transform()
    assertResult("11111000000")(actual)
  }

  test("part 1: 111100001010 becomes 1111000010100101011110000") {
    val solver = new Solver("111100001010", 25)
    val actual = solver.transform()
    assertResult("1111000010100101011110000")(actual)
  }

  test("part 1: checksum for 110010110100 is 100") {
    val solver = new Solver("110010110100", 25)
    val actual = solver.checksum()
    assertResult("100")(actual)
  }

  test("part 1: fill disk of length 20 from initial state 10000") {
    val solver = new Solver("10000", 20)
    val actual = solver.solve
    assertResult("01100")(actual)
  }
}
