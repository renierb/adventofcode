package part1

import scala.annotation.tailrec

class Solver(total: Int) {

  private val presents = Array.fill[Int](total)(1)

  def solve: Int = {
    takePresents(0)
  }

  @tailrec
  final def takePresents(taker: Int): Int = {
    if (presents(taker) == 0)
      takePresents((taker + 1) % total)
    else {
      val takee = takePresent(taker)
      if (takee == taker)
        taker + 1
      else {
        presents.update(taker, presents(taker) + presents(takee))
        presents.update(takee, 0)

        takePresents((takee + 1) % total)
      }
    }
  }

  private def takePresent(taker: Int) = {
    val index = presents.indexWhere(_ != 0, taker + 1)
    if (index >= 0)
      index
    else
      presents.indexWhere(_ != 0, 0)
  }
}