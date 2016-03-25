<?php
	// Connexion à la base de données
	try {
		
		$bdd = new PDO('mysql:host=localhost;dbname=minichat;charset=utf8', 'root', '');
	}
	catch(Exception $e) {

	    die('Erreur : '.$e->getMessage());
	}

	// Insertion du message à l'aide d'une requête préparée
	$req = $bdd->prepare('INSERT INTO users (pseudo, message, date) VALUES(?, ?, ?)');
	
	$_POST['pseudo'] = htmlspecialchars($_POST['pseudo']);
	$_POST['message'] = htmlspecialchars($_POST['message']);

	$req->execute(array($_POST['pseudo'], $_POST['message'], date("Y/m/d H:i:s")));

	// Redirection du visiteur vers la page du minichat
	header('Location: minichat.php?pseudo='.$_POST['pseudo']);
?>