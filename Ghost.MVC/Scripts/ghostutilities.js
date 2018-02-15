Ghost = window.Ghost || {};

Ghost.Play = {
   
    setVisibilityOfHumanPlayerHelp: function () {
        var $humanPlayerHelp = $('#humanPlayerHelp'),
            $showHumanPlayerHelp = $('#showHumanPlayerHelp');

        if ($showHumanPlayerHelp.is(':checked') && $humanPlayerHelp.hasClass('invisible')) {
            $humanPlayerHelp.removeClass('invisible');
        }
        else if (!$showHumanPlayerHelp.is(':checked') && !$humanPlayerHelp.hasClass('invisible')) {
            $humanPlayerHelp.addClass('invisible');
        }
    },

    getWordPlusNewMove: function () {
        var me = this,
            newMoveValue = $('#NewMove').val().trim(),
            wordAreaText = $('#wordArea').text().trim().replace(/(\r\n|\n|\r|[ ])/gm, "");

        if ((newMoveValue.length > 0) && (newMoveValue[0] !== '')) {
            return wordAreaText + newMoveValue[0].substr(0, 1);
        } else {
            return wordAreaText;
        }
    },

    getFinalValue: function (input, key) {
        var me = this,
            beforeSelection = input.value.substr(0, input.selectionStart),
            afterSelection = input.value.substr(input.selectionEnd, input.value.length - input.selectionEnd);
        
        return beforeSelection + key + afterSelection;
    },

    validateEntry: function (value, validationPattern) {
        var me = this,
            re = new RegExp(validationPattern);            

        if ((value !== '') && (!re.test(value))) {
            return false;
        }

        return true;
    },

    getStateAnalysis: function (callback) {
        var me = this,
            url = 'http://localhost:50519/api/Analyse',
            word = me.getWordPlusNewMove(),
            state = JSON.stringify({ Word: word });

        $.ajax({
            url: url,
            type: "POST",
            data: state,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //success: function (data) { console.log(word + "-> "); if (data) console.log(data.Help); else console.log("Nada"); }
            success: function (data) {
                me.buildAndSetHumanPlayerHelpMessage.call(me, data);
            }
        });
    },

    //function buildAndSetHumanPlayerHelpMessage(analysis) {
    buildAndSetHumanPlayerHelpMessage: function (analysis) {
        var me = this,
            $humanPlayerHelp = $('#humanPlayerHelp'),
            patternPlayer0 = new RegExp('Player 0', 'i'),
            patternPlayer1 = new RegExp('Player 1', 'i'),
            word = me.getWordPlusNewMove(),
            msg;

        if (!analysis) return "";

        if (analysis.ExpectedWinner == 1 || analysis.Winner == 1) {
            msg = 'For word "' + word + '", you will loose';
        } else {
            msg = 'For word "' + word + '", ' + analysis.Help;
            msg = msg.replace(patternPlayer0, 'you');
            msg = msg.replace(patternPlayer1, 'your opponent');
        }
        $humanPlayerHelp.text(msg);
    },
};


$(document).ready(function () {
    var $showHumanPlayerHelp = $('#showHumanPlayerHelp'),
        $newMove = $('#NewMove');

    $showHumanPlayerHelp.change(Ghost.Play.setVisibilityOfHumanPlayerHelp);
    Ghost.Play.setVisibilityOfHumanPlayerHelp();    

    $newMove.val('');

    $newMove.keypress(function (event) {        
        event.preventDefault();
        if (!Ghost.Play.validateEntry(event.key, "^[a-zA-Z]$")) {                        
            return;
        };
        event.target.value = event.key;
        Ghost.Play.getStateAnalysis(Ghost.Play.buildAndSetHumanPlayerHelpMessage);
    })
});        
