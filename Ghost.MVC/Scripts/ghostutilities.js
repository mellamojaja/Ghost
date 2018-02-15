Ghost = Ghost || {};

Ghost.Play = (function () {
    
        function setVisibilityOfHumanPlayerHelp() {
            var $humanPlayerHelp = $('#humanPlayerHelp'),
                $showHumanPlayerHelp = $('#showHumanPlayerHelp');

            if ($showHumanPlayerHelp.is(':checked') !== $humanPlayerHelp.is(':visible')) {
                if ($showHumanPlayerHelp.val()) {
                    $humanPlayerHelp.addClass('visible');
                }
                else {
                    $humanPlayerHelp.addClass('invisible');
                }
            }
        }

        function getWordPlusNewMove() {
            var newMoveValue = $('#newMove').val().trim(),
                wordAreaText = $('#wordArea').text().trim();

            if ((newMoveValue.length > 0) && (newMoveValue[0] !== '')) {
                return wordAreaText + newMoveValue[0].substr(0, 1);
            } else {
                return wordAreaText;
            }
        }

        function getStateAnalysis(callback) {
            var url = 'http://localhost:50519/api/Analyse',
                word = getWordPlusNewMove();

            $.post(url, { Word: word }, callback);
        }

        function buildAndSetHumanPlayerHelpMessage(analysis) {
            var $humanPlayerHelp = $('#humanPlayerHelp'),
                re = new RegExp('Player 0', 'i'),
                word = getWordPlusNewMove(),
                msg;

            msg = 'For word "' + word + '", ' + analysis.Help;
            msg = msg.replace(re, 'you');
            $humanPlayerHelp.text(msg);
        }

        (function ($) {
            $(document).ready(function () {
                var $showHumanPlayerHelp = $('#showHumanPlayerHelp'),
                    $newMove = $('#newMove');

                $showHumanPlayerHelp.change(setVisibilityOfHumanPlayerHelp);

                $newMove.change(function () {
                    getStateAnalysis(buildAndSetHumanPlayerHelpMessage);
                })
            });
        })($);
    
})(); 
