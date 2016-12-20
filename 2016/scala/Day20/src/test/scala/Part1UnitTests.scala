import org.scalatest._
import part1.Solver

import scala.collection.immutable.SortedSet

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val solver = new Solver(SortedSet[(Long, Long)]((5L,8L), (0L,2L), (4L,7L)))
    val actual = solver.solve
    assertResult(3)(actual)
  }
}
