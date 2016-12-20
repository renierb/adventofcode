package part2

import java.util

import scala.annotation.tailrec
import scala.collection.mutable
import scala.collection.mutable.ArrayBuffer

class Solver(total: Int) {

  private val presents: ArrayBuffer[Int] = ArrayBuffer(ArrayBuffer.fill[Int](total)(1).indices.map(_ + 1): _*)

  def solve: Int = {
    takePresents(0, total)
  }

  @tailrec
  final def takePresents(taker: Int, elves: Int = total): Int = {
    require(elves >= 1)
    if (elves == 1)
      presents.filter(_ != 0).head
    else if (presents(taker) == 0)
      takePresents((taker + 1) % total, elves)
    else {
      var takee = takeeIndex(taker, elves / 2)

      presents.update(takee, 0)
      takePresents((taker + 1) % total, elves - 1)
    }
  }

  @tailrec
  private def takeeIndex(from: Int, skip: Int): Int = {
    if (presents(from) == 0)
      takeeIndex((from + 1) % total, skip)
    else {
      if (skip == 0)
        from
      else
        takeeIndex((from + 1) % total, skip - 1)
    }
  }
}