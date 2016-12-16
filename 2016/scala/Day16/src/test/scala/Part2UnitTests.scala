import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2: 1 becomes 100") {
    val solver = new Solver("1", 3)
    val actual = solver.transform().mkString
    assertResult("100")(actual)
  }

  test("part 2: 0 becomes 001") {
    val solver = new Solver("0", 3)
    val actual = solver.transform().mkString
    assertResult("001")(actual)
  }

  test("part 2: 11111 becomes 11111000000") {
    val solver = new Solver("11111", 11)
    val actual = solver.transform().mkString
    assertResult("11111000000")(actual)
  }

  test("part 2: 111100001010 becomes 1111000010100101011110000") {
    val solver = new Solver("111100001010", 25)
    val actual = solver.transform().mkString
    assertResult("1111000010100101011110000")(actual)
  }

  test("part 2: checksum for 110010110100 is 100") {
    val solver = new Solver("110010110100", 25)
    val actual = solver.checksum().mkString
    assertResult("100")(actual)
  }

  test("part 2: fill disk of length 20 from initial state 10000") {
    val solver = new Solver("10000", 20)
    val actual = solver.solve
    assertResult("01100")(actual)
  }

  test("part 2: fill disk of length 35651584 from initial state 01111010110010011") {
    val solver = new Solver("01111010110010011", 35651584)
    val actual = solver.solve
    assertResult("11101110011100110")(actual)
  }
}
