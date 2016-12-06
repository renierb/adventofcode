package solution.part1

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
      val md5 = MD5(digest, s"$doorID$from")
      if (md5.startsWith("00000")) {
        solve(from + 1, until, PasswordItem(from, md5(5)) :: answers)
      } else {
        solve(from + 1, until, answers)
      }
    }
  }

  def MD5(digest: MessageDigest, text: String): String = {
    digest.digest(text.getBytes).take(3).map("%02x".format(_)).mkString
  }
}