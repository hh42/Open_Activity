<!DOCTYPE html>

<?php 
// changer cette valeur pour faciliter le test de la pagination
$nombreDeMessageParPage = 10;

// je recupere le pseudo envoyé via l'url s'il y en a
if ($_GET && $_GET['pseudo']) {
    $pseudo = htmlspecialchars($_GET['pseudo']);
} else {
    $pseudo = '';
}

// je recupere le numero de la page actuel commençant à 0
if ($_POST && $_POST['page']) {
    $page = htmlspecialchars($_POST['page']);
} else {
    $page = 0;
}

// Connexion à la base de données
try {

    $bdd = new PDO('mysql:host=localhost;dbname=minichat;charset=utf8', 'root', '');
} 
catch(Exception $e) {

        die('Erreur : '.$e->getMessage());
}

?>

<html>
    <head>
        <meta charset="utf-8" />
        <title>Mini-chat</title>
    </head>

    <style>
        form{text-align:center;}
    </style>

    <body>

        <form action="minichat_post.php" method="post">
            <p>
                <!-- required ajouté pour forcer le remplissage du champ pseudo -->
                <label for="pseudo">Pseudo</label> : <input type="text" name="pseudo" id="pseudo" value="<?php echo $pseudo ?>" required/><br />
                <label for="message">Message</label> :  <input type="text" name="message" id="message" /><br />
                <input type="submit" value="Envoyer" />
            </p>
        </form>

        <form action="minichat_post.php" method="post">
            <input type="hidden" name="pseudo" id="hidden" value="<?php echo $pseudo ?>" required/><br />
            <input type="submit" value="Rafraichir la page" />
            </form>

        <div class="pagination">
             <form action="minichat.php" method="post">

                <?php 
                    /* 
                        - Si on est à la page 0 je cache la page précédent
                        - Je vérifie que la page suivant contient des commentaires
                        - Si la requête retourne 0 en resultat, je cache la page suivant
                    */

                    if ($page > 0) {
                        echo '<input type="submit" class="button" name="page" value="'.($page - 1).'"/>';
                    }

                    $reponse = $bdd->query('SELECT COUNT(id) FROM users WHERE id BETWEEN '. ($page + 1) * $nombreDeMessageParPage .' AND '. (($page + 1) * $nombreDeMessageParPage + $nombreDeMessageParPage));
                    if ($reponse->fetch()[0]){
                        echo '<input type="submit" class="button" name="page" value="'.($page + 1).'"/>';
                    }
                    $reponse->closeCursor();
                ?>
            </form>
            <?php echo 'Page : '.$page ?>
        </div>

            <?php

            // Récupération des infos de $nombreDeMessageParPage demandé
            $reponse = $bdd->query('SELECT pseudo, message, DATE_FORMAT(date, "%d/%m/%Y %H:%i:%S") AS date FROM users ORDER BY ID DESC LIMIT '. $page * $nombreDeMessageParPage .','.  $nombreDeMessageParPage);

            // Affichage de chaque message (toutes les données sont protégées par htmlspecialchars)
            while ($donnees = $reponse->fetch()) {

                echo '<p>'.
                        htmlspecialchars($donnees['date']).
                        ' <strong>' . htmlspecialchars($donnees['pseudo']) . 
                        '</strong> : ' . htmlspecialchars($donnees['message']) . 
                    '</p>';
            }
            $reponse->closeCursor();

            ?>

    </body>
</html>