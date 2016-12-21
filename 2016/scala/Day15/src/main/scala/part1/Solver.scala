package part1

import part1.InputParser._

import scala.annotation.tailrec

class Solver(input: List[Disc]) extends DomainDef {

  def solve: Int = {
    val whenCapsuleIsThrough = iterate(SnapshotInTime(0, input))
    val whenToPressTheButton = whenCapsuleIsThrough - input.length
    whenToPressTheButton
  }

  @tailrec
  private def iterate(state: SnapshotInTime, time: Int = 0): Int = {
    if (time > 0 && state.hasPassedThrough)
      time
    else
      iterate(state.tickOneSecond, time + 1)
  }
}
