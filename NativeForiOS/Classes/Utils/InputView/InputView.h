//
//  InputView.h
//  Unity-iPhone
//
//  Created by 玩贝 on 2018/12/20.
//

#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface InputView : UIView

- (instancetype)initWithFrame:(CGRect)frame Edit:(int )mId limit:(int)limit str:(NSString *)str hint:(NSString *)hint tag:(NSString *)tag;

-(void)showEdit;

-(void)allScreenBtnHandler;

-(NSString*)getInputAreaHeight;

-(NSString*)getInputAreaConent;

@end

NS_ASSUME_NONNULL_END
