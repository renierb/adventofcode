package part2

import akka.pattern.ask

import scala.annotation.tailrec
import scala.concurrent.Await

class Solver(input: String, chips: Seq[Int]) extends part1.Solver(input, chips) {

  override def solve: Int = {
    @tailrec
    def traverse(actions: Actions): Unit = {
      if (actions.isEmpty) {
        system.terminate()
        None
      } else {
        val action = actions.head
        action match {
          case Give(chipId, botId) =>
            // akka ask pattern ...
            Await.result(bots(botId) ? Chip(chipId), timeout.duration)
            traverse(actions.tail)
        }
      }
    }

    traverse(actions)
    bins(0).chips.head * bins(1).chips.head * bins(2).chips.head
  }
}
