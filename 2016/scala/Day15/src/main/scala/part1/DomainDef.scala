package part1

import part1.InputParser.Disc

import scala.annotation.tailrec

trait DomainDef {

  case class SnapshotInTime(time: Int, discs: List[Disc]) {

    def tickOneSecond: SnapshotInTime = {
      val newState = for {
        d <- discs
      } yield Disc(d.nr, d.positions, (d.position + 1) % d.positions)

      SnapshotInTime(time + 1, newState)
    }

    def hasPassedThrough: Boolean = {
      time > 0 && hasPassedThrough(discs, discs.length - 1)
    }

    @tailrec
    private def hasPassedThrough(discs: List[Disc], position: Int): Boolean = {
      if (discs.isEmpty)
        true
      else {
        val disc = discs.head
        if (disc.position != position % disc.positions)
          false
        else
          hasPassedThrough(discs.tail, position - 1)
      }
    }
  }
}
