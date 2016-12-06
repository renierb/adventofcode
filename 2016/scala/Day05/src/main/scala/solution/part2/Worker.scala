package solution.part2

import java.security.MessageDigest

import akka.actor.Actor
import solution.{PasswordItem, Result, Work}

import scala.annotation.tailrec

class Worker(doorID: String) extends Actor {
  private val digest = MessageDigest.getInstance("MD5")

  def receive: Receive = {
    case Work(from, until) =>
      println(s"Receive request: $from until $until")
      val answers = solve(from, until)
      sender ! Result(answers)
  }

  @tailrec
  final def solve(from: Int, until: Int, answers: List[PasswordItem] = List()): List[PasswordItem] = {
    if (from >= until) {
      answers
    } else {
      val answer = getCharFor(from)
      if (answer.isDefined) {
        solve(from + 1, until, answer.get :: answers)
      } else {
        solve(from + 1, until, answers)
      }
    }
  }

  def getCharFor(i: Int): Option[PasswordItem] = {
    val hash = MD5(s"$doorID$i")
    if (hash.startsWith("00000")) {
      val index = hash(5)
      if (index >= 48 && index <= 55)
        Some(PasswordItem(i, hash(6), index.toString.toInt))
      else
        None
    } else {
      None
    }
  }

  def MD5(text: String): String = {
    digest.digest(text.getBytes).map("%02x".format(_)).mkString
  }
}
