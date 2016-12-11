package part1

import akka.pattern.ask

import scala.annotation.tailrec
import scala.concurrent.Await

class Solver(input: String, chips: Seq[Int]) extends DomainDef {

  Instructions.parse(input)

  protected val actions: List[Action] = getActions

  def solve: Int = {
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
        }
        traverse(actions.tail)
      }
    }

    traverse(actions)
    answers.find(_._2.intersect(chips).length == 2).map(p => p._1).getOrElse(-1)
  }
}