-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Ven 25 Mars 2016 à 00:07
-- Version du serveur :  5.6.17
-- Version de PHP :  5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `minichat`
--

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pseudo` varchar(255) NOT NULL,
  `message` varchar(255) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Contenu de la table `users`
--

INSERT INTO `users` (`id`, `pseudo`, `message`, `date`) VALUES
(1, 'toto', 'Bonjour a tous, voici mon tp .... dites moi ce que vous en pensez', '2016-03-25 00:05:17'),
(2, 'titi', 'Quoi de neuf sur ce tp ?', '2016-03-25 00:05:27'),
(3, 'toto', 'bah, la pagination, le remplissage du pseudo et le bouton pour rafraichir la page ...', '2016-03-25 00:05:34'),
(4, 'titi', 'A oui c''est vrai ! on va pouvoir tout casser hihih :p', '2016-03-25 00:05:42'),
(5, 'toto', 'C''est pas drôle -_-''', '2016-03-25 00:05:49'),
(6, 'titi', 'Je rigole, c''est partie pour la correction :)', '2016-03-25 00:05:57'),
(7, 'toto', 'J''ai fixé le nombre de message à 10 par page, libre à vous de le changer.', '2016-03-25 00:06:19'),
(8, 'titi', 'Et la dates au format français ?', '2016-03-25 00:06:30'),
(9, 'toto', 'Oui, il y a ça aussi ... voyez devant les pseudo :D', '2016-03-25 00:06:44');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
