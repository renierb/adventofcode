package part2

import scala.annotation.tailrec
import scala.collection.mutable.ListBuffer
import scala.collection.parallel.ParSet

class Solver(total: Int) {

  private val elves: ListBuffer[Int] = ListBuffer(ListBuffer.fill[Int](total)(0).indices.map(_ + 1): _*)

  def solve: Int = {
    takePresents(0, elves.length)
  }

  @tailrec
  private def takePresents(index: Int, count: Int, removed: ParSet[Int] = ParSet()): Int = {
    val taker = elves.head
    if (elves.length == 1)
      taker
    else {
      if (removed.contains(taker)) {
        elves.remove(0)
        takePresents(index - 1, count, removed - taker)
      } else {
        val takee = elves(index + (count / 2))

        elves.remove(0)
        elves += taker

        takePresents(index + 1, count - 1, removed + takee)
      }
    }
  }
}