package part1

import java.security.MessageDigest

import scala.annotation.tailrec
import scala.concurrent.{Future, Promise}
import scala.concurrent.ExecutionContext.Implicits.global
import scala.util.{Failure, Success}
//import scala.async.Async.{async, await}

class Solver(doorID: String) {

  def solveParallel(threads: Int): String = {
    val range = Int.MaxValue / threads

    val futs = (0 until threads).map(i => Future {
      solve(range*i, range*(i+1))
    }).toArray

    var password = ""
    val aggFuts = processFutures(futs, List(), Promise[List[String]]())
    val result = aggFuts onComplete {
      value => password = value.get.mkString
    }
    password.take(8)
  }

  def processFutures[T](futures: Array[Future[T]], values: List[T], prom: Promise[List[T]]): Future[List[T]] = {
    val fut = futures.head

    fut onComplete {
      case Success(value) if futures.length == 1 =>
        prom.success(value :: values)

      case Success(value) =>
        processFutures(futures.tail, value :: values, prom)

      case Failure(ex) => prom.failure(ex)
    }
    prom.future
  }

  @tailrec
  final def solve(from: Int = 0, until: Int = Int.MaxValue, acc: String = ""): String = {
    if (acc.length == 8 || from >= until) {
      acc
    } else {
      val md5 = MD5(s"$doorID$from")
      val char: Option[Char] =
        if (md5.startsWith("00000") && md5(5) != '0')
          Some(md5(5))
        else
          None
      solve(from + 1, until, acc + char.getOrElse(""))
    }
  }

  def MD5(text: String): String = {
    val digest = MessageDigest.getInstance("MD5")
    digest.digest(text.getBytes).map("%02x".format(_)).mkString
  }
}
